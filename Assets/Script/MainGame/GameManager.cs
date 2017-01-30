using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    /// <summary>ゲームマネージャー(シングルトン) </summary>
    public class GameManager : SingletonMonoBehaviourFast<GameManager>
    {
        public  GameObject EmperorBattlePlace;
        public  GameObject SlaveBattlePlace;

        private CardInfo   EmperorSideSelect;
        private CardInfo   SlavesSideSelect;

        void Start()
        {

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
                case PlayerSide.Emperor: EmperorSideSelect = card; break;
                case PlayerSide.Slave:   SlavesSideSelect  = card; break;
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

        public void MoveCardBattlePlace(CardInfo card)
        {
            GameObject battlePlace = GetBattlePlace(card.side);
            Vector3    moveTo      = battlePlace.transform.position;

            StartCoroutine(MoveCard(card, moveTo));
            SetBattlePlace(card);

            //既に選択しているカードがあるか
            CardInfo selectedCard = GetSelectCard(card.side);
            if(selectedCard != null)
            {
                moveTo = card.card.transform.position;
                StartCoroutine(MoveCard(selectedCard, moveTo));
            }
        }

        private IEnumerator MoveCard(CardInfo card, Vector3 to)
        {
            card.card.transform.position = to;
            yield return null;
        }

        /// <summary> カードの情報を送る </summary>
        /// <param name="info"> カードの情報 </param>
        /// <returns> 送信に成功したか </returns>
        public bool SendCardServer(CardInfo info)
        {
            return false;
        }

        /// <summary> サーバーからデータを受け取る </summary>
        /// <param name="side"> プレイヤーのサイド </param>
        /// <returns> データの受け取りに成功したか </returns>
        public bool ReceiveCardServer(PlayerSide side)
        {
            return false;
        }

        /// <summary> 勝敗判定 </summary>
        /// <returns> 勝ったプレイヤーのサイド </returns>
        public PlayerSide Judge()
        {
            throw null;
        }
    }
}