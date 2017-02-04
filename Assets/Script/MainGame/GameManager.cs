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
                Debug.Log("trade");
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
        /// 受け取り完了したときに呼ばれる関数
        /// </summary>
        /// <param name="">受け取ったデータ</param>
        void OnReceiveJob(string job)
        {
            Debug.Log("Receive");
            JobClass jobClass = (JobClass)System.Enum.Parse(typeof(JobClass), job);


        }
    }
}