using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace GameMain
{
    /// <summary>ゲームマネージャー(シングルトン) </summary>
    public class GameManager : SingletonMonoBehaviourFast<GameManager>
    {
        public  GameObject EmperorBattlePlace;
        public  GameObject SlaveBattlePlace;

        private CardInfo   EmperorSideSelect;
        private CardInfo   SlavesSideSelect;

        [SerializeField]
        private PlayerSelectArea playerSelectArea;

        void Start()
        {
            EmperorSideSelect = new CardInfo();
            SlavesSideSelect  = new CardInfo();
        }

        void Update()
        {

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

        void SetBattlePlace(CardInfo card)
        {
            switch(card.side)
            {
                case PlayerSide.Emperor: EmperorSideSelect = card; return;
                case PlayerSide.Slave:   SlavesSideSelect  = card; return;
            }
            
            Debug.LogError("不正な値です");
            throw null;
        }

        CardInfo GetSelectCard(PlayerSide side)
        {
            switch(side)
            {
                case PlayerSide.Emperor: return EmperorSideSelect;
                case PlayerSide.Slave:   return SlavesSideSelect;
            }

            Debug.LogError("不正な値です");
            throw null;
        }

        /// <summary>プレイヤーが選択したサイドの取得 </summary>
        /// <returns>プレイヤーが選択したサイド</returns>
        PlayerSide GetActivePlayerSide()
        {
            //idから変換
            int id = PlayerPrefs.GetInt("Playerside");
            PlayerSide side = (PlayerSide)System.Enum.ToObject(typeof(PlayerSide), id);
            return side;
        }

        /// <summary> 敵サイドの情報の取得</summary>
        /// <returns> 敵サイドの情報 </returns>
        PlayerSide GetEnemyPlayerSide()
        {
            PlayerSide playerSide = GetActivePlayerSide();
            if(playerSide == PlayerSide.Emperor) return PlayerSide.Slave;
            if(playerSide == PlayerSide.Slave)   return PlayerSide.Emperor;

            throw null;
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
                Vector3 returnPos = card.card.transform.position;

                StartCoroutine(MoveCard(card.card.transform,       moveTo, 
                                        selectCard.card.transform, returnPos));
            }
            else
            {
                StartCoroutine(MoveCard(card, moveTo));
            }
            
            SetBattlePlace(card);
        }

        private IEnumerator MoveCard(CardInfo card, Vector3 to)
        {
            card.card.transform.position = to;
            yield return null;
        }

        private IEnumerator MoveCard(Transform selectCard,   Vector3 battlePlacePosition,
                                     Transform unSelectCard, Vector3 selectCardPosition)
        {
            selectCard.position   = battlePlacePosition;
            unSelectCard.position = selectCardPosition;

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

            //送信
            CardInfo selectCard = GetSelectCard(side);
            SendCardServer(selectCard);
            
            //相手の選択待ち
            PlayerSide enemySide = GetEnemyPlayerSide();
            ReceiveCardServer(enemySide);


        }

        /// <summary> カードの情報を送る </summary>
        /// <param name="info"> カードの情報 </param>
        public void SendCardServer(CardInfo info)
        {
            StartCoroutine(SetJob(info));
        }

        /// <summary> サーバーからデータを受け取る </summary>
        /// <param name="side"> プレイヤーのサイド </param>
        public void ReceiveCardServer(PlayerSide side)
        {
            StartCoroutine(GetJob(side));
        }

        /// <summary> 勝敗判定 </summary>
        /// <returns> 勝ったプレイヤーのサイド </returns>
        public PlayerSide Judge()
        {

            bool[][] JudgeGraph = new bool[][]
            {
                new bool[] {   },
                new bool[] {   },
                new bool[] {   }
            };

            throw null;
        }

        /// <summary>
        /// カードをPHPに送り出す
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IEnumerator SetJob(CardInfo info)
        {
            UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)info.side + "DataSave.php?class=" + info.job.ToString());
            yield return request.Send();

            // 何らかのエラーがあったら
            if (request.isError)
            {
                // エラー処理
                Debug.Log("エラー");
            }
            else {
                Debug.Log(request.responseCode);
                // レスポンスコードを見る
                if (request.responseCode == 200)
                {
                    string test = request.downloadHandler.text;
                    Debug.Log(test);
                }
            }
        }

        /// <summary>
        /// カードをPHPから受け取る
        /// </summary>
        /// <returns></returns>
        IEnumerator GetJob(PlayerSide side)
        {
            UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)side+ "DataGet.php");
            yield return request.Send();

            // 何らかのエラーがあったら
            if (request.isError)
            {
                // エラー処理
                Debug.Log("エラー");
            }
            else {
                Debug.Log(request.responseCode);
                // レスポンスコードを見る
                if (request.responseCode == 200)
                {
                    string test = request.downloadHandler.text;
                    if (test == "") StartCoroutine(GetJob(side));
                    if (side == PlayerSide.Emperor)
                    {
                        EmperorSideSelect.job = (JobClass)System.Enum.Parse(typeof(JobClass), test);
                    }
                    else
                    {
                        Debug.Log(test);
                        SlavesSideSelect.job  = (JobClass)System.Enum.Parse(typeof(JobClass), test);
                    }
                    Debug.Log("受け取ったデータ = " + test);
                }
            }
        }

    }
}