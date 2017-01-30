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
                        EmperorSideSelect.job = (JobClass)int.Parse(test);
                    }
                    else
                    {
                        SlavesSideSelect.job = (JobClass)int.Parse(test);
                    }
                    Debug.Log("受け取ったデータ = " + (JobClass)int.Parse(test));
                }
            }
        }

    }
}