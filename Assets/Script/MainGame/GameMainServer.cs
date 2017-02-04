using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace GameMain
{
    public class GameMainServer : MonoBehaviour
    {
        //[SerializeField, Tooltip("説明文")]
        GameManager manager;

        void Awake()
        {
            manager = GetComponent<GameManager>();
        }

        void Start()
        {

        }

        /// <summary>
        /// サーバーのデータの初期化
        /// </summary>
        public void InitServerData()
        {
            StartCoroutine(Initialize());
        }

        /// <summary> カードの情報を送る </summary>
        /// <param name="info"> カードの情報 </param>
        public void SendCardServer(CardInfo info)
        {
            StartCoroutine(SetJob(info));
        }

        /// <summary> サーバーからデータを受け取る </summary>
        /// <param name="side"> プレイヤーのサイド </param>
        public void ReceiveCardServer(PlayerSide side, ReceiveDataFunc callback)
        {
            StartCoroutine(GetJob(side, callback));
        }

        /// <summary>
        /// 何も無い情報をphpに送って初期化
        /// </summary>
        /// <returns></returns>
        private IEnumerator Initialize()
        {
            UnityWebRequest request;
            request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player1DataSave.php?class=");
            yield return request.Send();

            request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player2DataSave.php?class=");
            yield return request.Send();
        }

        /// <summary>
        /// カードをPHPに送り出す
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private IEnumerator SetJob(CardInfo info)
        {
            Debug.Log("ﾌﾟﾚｲﾔｰサイド = " + info.side);
            Debug.Log("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)info.side + "DataSave.php?class=" + info.job.ToString());
            UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)info.side + "DataSave.php?class=" + info.job.ToString());
            yield return request.Send();

            // 何らかのエラーがあったら
            if (request.isError)
            {
                // エラー処理
                Debug.Log("エラー");
            }
            else
            {
                Debug.Log(request.responseCode);
                // レスポンスコードを見る
                if (request.responseCode == 200)
                {
                    string test = request.downloadHandler.text;
                    Debug.Log(test);
                }
            }
        }

        public delegate void ReceiveDataFunc(string data);

        /// <summary>
        /// カードをPHPから受け取る
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetJob(PlayerSide side, ReceiveDataFunc callback)
        {
            Debug.Log("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)side+ "DataGet.php");
            UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/" + "Player"+ (int)side+ "DataGet.php");
            yield return request.Send();

            // 何らかのエラーがあったら
            if (request.isError)
            {
                // エラー処理
                Debug.Log("エラー");
            }
            else
            {
                Debug.Log(request.responseCode);
                // レスポンスコードを見る
                if (request.responseCode == 200)
                {
                    string test = request.downloadHandler.text;
                    if (test == "")
                    {
                        yield return new WaitForSecondsRealtime(3.0f);
                        StartCoroutine(GetJob(side, callback));
                    }
                    Debug.Log(test);
                    if (side == PlayerSide.Emperor)
                    {
                        manager.EmperorSideSelect.job = (JobClass)System.Enum.Parse(typeof(JobClass), test);
                    }
                    else
                    {
                        manager.SlavesSideSelect.job  = (JobClass)System.Enum.Parse(typeof(JobClass), test);
                    }

                    callback(test);
                    Debug.Log("受け取ったデータ = " + test);
                }
            }
        }
    }
}