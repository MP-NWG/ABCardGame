using UnityEngine;
using System.Collections;

namespace GameMain
{
    /// <summary>ゲームマネージャー(シングルトン) </summary>
    public class GameManager : SingletonMonoBehaviourFast<GameManager>
    {
        public  GameObject EmperorCardPack;
        public  GameObject SlaveCardPack;

        public  GameObject EmperorBattlePlace;
        public  GameObject SlaveBattlePlace;

        public  SceneChanger sceneChanger;

        private CardInfo emperorSideSelect;
        public  CardInfo EmperorSideSelect
        {
            get { return emperorSideSelect; }
        }

        private CardInfo slavesSideSelect;
        public  CardInfo SlavesSideSelect
        {
            get { return slavesSideSelect; }
        }

        [SerializeField]
        private PlayerSelectArea playerSelectArea;
        

        [SerializeField]
        private GameObject waitingMessage;

        GameMainServer server;

        void Start()
        {
            server = GetComponent<GameMainServer>();
            emperorSideSelect = new CardInfo();
            slavesSideSelect  = new CardInfo();
            SendInitSaver();
        }

        GameObject GetBattlePlace(PlayerSide side)
        {
            switch(side)
            {
                case PlayerSide.Emperor: return EmperorBattlePlace;
                case PlayerSide.Slave:   return SlaveBattlePlace;
            }

            Debug.LogError("不正な値です");
            throw null;
        }

        void SetBattlePlaceCard(CardInfo card)
        {
            switch(card.side)
            {
                case PlayerSide.Emperor: emperorSideSelect = card; return;
                case PlayerSide.Slave:   slavesSideSelect  = card; return;
            }
            
            Debug.LogError("不正な値です");
            throw null;
        }

        CardInfo GetSelectCard(PlayerSide side)
        {
            switch(side)
            {
                case PlayerSide.Emperor: return emperorSideSelect;
                case PlayerSide.Slave:   return slavesSideSelect;
            }

            Debug.LogError("不正な値です");
            throw null;
        }

        /// <summary>プレイヤーが選択したサイドの取得 </summary>
        /// <returns>プレイヤーが選択したサイド</returns>
        PlayerSide GetActivePlayerSide()
        {
            return PlayerData.instance.side;
        }

        /// <summary> 敵サイドの情報の取得 </summary>
        /// <returns> 敵サイドの情報 </returns>
        PlayerSide GetEnemyPlayerSide()
        {
            PlayerSide playerSide = GetActivePlayerSide();
            if(playerSide == PlayerSide.Emperor) return PlayerSide.Slave;
            if(playerSide == PlayerSide.Slave)   return PlayerSide.Emperor;

            throw null;
        }

        /// <summary> 敵のカードをセット </summary>
        /// <param name="jobClass">セットするカードの役職</param>
        void EnemySetCard(JobClass jobClass)
        {
            PlayerSide side = GetEnemyPlayerSide();

            //相手のカードを取得
            GameObject enemyCards = null;
            switch(side)
            {
                case PlayerSide.Emperor: enemyCards = EmperorCardPack; break;

                case PlayerSide.Slave:   enemyCards = SlaveCardPack;   break;
            }

            //カードに変換
            Transform enemySelectedObj = enemyCards.transform.FindChild(jobClass.ToString());
            CardInfo  enemySelecteCard = enemySelectedObj.GetComponent<SelectCard>().info;

            SetBattlePlaceCard(enemySelecteCard);

            //敵のカードをバトル場にセット
            GameObject moveTo = GetBattlePlace(enemySelecteCard.side);
            StartCoroutine(MoveCard(enemySelecteCard, moveTo.transform.position));
        }

        public void MoveCardBattlePlace(CardInfo card)
        {
            GameObject battlePlace = GetBattlePlace(card.side);
            Vector3    moveTo      = battlePlace.transform.position;
            
            Debug.Log(card.ToString());

            //既に選択しているカードがあるか
            CardInfo selectCard = GetSelectCard(card.side);
            if(selectCard.card != null)
            {
                Debug.Log("trade");
                Vector3 returnPos = card.card.transform.position;

                StartCoroutine(MoveCard(card.card.transform,       moveTo, 
                                        selectCard.card.transform, returnPos));
            }
            else
            {
                StartCoroutine(MoveCard(card, moveTo));
            }
            
            SetBattlePlaceCard(card);
        }

        private IEnumerator MoveCard(CardInfo card, Vector3 to)
        {
            yield return StartCoroutine(MoveCard(card.card.transform, to));
        }

        private IEnumerator MoveCard(Transform card, Vector3 to)
        {
            card.position = to;
            yield return null;
        }

        private IEnumerator MoveCard(Transform selectCard,   Vector3 battlePlacePosition,
                                     Transform unSelectCard, Vector3 selectCardPosition)
        {
            selectCard.position   = battlePlacePosition;
            unSelectCard.position = selectCardPosition;

            Debug.Log("trade end");

            yield return null;
        }

        /// <summary> カードを確定し、サーバーに送る </summary>
        public void ConfirmCard()
        {
            PlayerSide side = GetActivePlayerSide();

            if(GetSelectCard(side) == null)
            {
                Debug.Log("選択されているカード無し");
                return;
            }

            //再度選択できないようにする
            playerSelectArea.cardSelect = false;

            //表示
            waitingMessage.SetActive(true);

            //送信
            CardInfo selectCard = GetSelectCard(side);
            server.SendCardServer(selectCard);
            
            //相手の選択待ち
            PlayerSide enemySide = GetEnemyPlayerSide();
            server.ReceiveCardServer(enemySide, OnReceiveJob);
        }

        void SendInitSaver()
        {
            if(GetActivePlayerSide() == PlayerSide.Slave)
            {
                //奴隷側がServerに初期化処理を送る
                //まぁ 奴隷だもんね
                server.InitServerData();
            }
        }

        /// <summary> 受け取り完了したときに呼ばれる関数 </summary>
        /// <param name="">受け取ったデータ</param>
        void OnReceiveJob(string job)
        {
            Debug.Log("Receive");
            JobClass jobClass = (JobClass)System.Enum.Parse(typeof(JobClass), job);

            EnemySetCard(jobClass);

            //接続待ち表示を消す
            waitingMessage.SetActive(false);

            StartCoroutine(ShowGameResult());
        }

        /// <summary> 勝敗判定 </summary>
        /// <returns> プレイヤー側が勝ったか null=引き分け </returns>
        public bool? Judge(JobClass self, JobClass enemy)
        {
            //true  勝ち
            //false 負け
            //null  引き分け
            bool?[][] JudgeTable = new bool?[][]
            {
                //自分        市民    皇帝    奴隷      // 相手
                new bool?[] { null,  true, false },   // 市民
                new bool?[] {false,  null,  true },   // 皇帝
                new bool?[] { true, false,  null }    // 奴隷
            };
            
            return JudgeTable[(int)self][(int)enemy];
        }
        
        IEnumerator ShowGameResult()
        {
            Coroutine wait = StartCoroutine(ShowEnemyCard());
            yield return wait;
            
            CardInfo self  = GetSelectCard(GetActivePlayerSide());
            CardInfo enemy = GetSelectCard(GetEnemyPlayerSide());
            
            bool? result = Judge(self.job, enemy.job);

            yield return new WaitForSeconds(5.0f);

            TurnEnd(result);
        }

        IEnumerator ShowEnemyCard()
        {
            //ここにオープンアニメーション
            CardInfo  info       = GetSelectCard(GetEnemyPlayerSide());
            Transform enemyCard  = info.card.transform;
            enemyCard.localScale = new Vector3(1, 1);
            yield return null;
        }

        void TurnEnd(bool? result)
        {
            if(result.HasValue) //勝敗がついた
            {
                if(result.Value)
                {
                    //勝利
                    PlayerData.Instance.point++;
                }

                
                //この試合での勝敗がついたので次へ
                //シーン移動
                //sceneChanger.SceneChange();
                return;
            }
            else //引き分け
            {
                //勝敗がつくまで
                //初期化
                SendInitSaver();
                Destroy(emperorSideSelect.card);
                Destroy(slavesSideSelect .card);
                playerSelectArea.cardSelect = true;
            }
        }
    }
}