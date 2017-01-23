using UnityEngine;
using System.Collections;
// 追加
using UnityEngine.Networking;

using System;
using System.IO;
using System.Net;
using System.Text;

public class GameManager : MonoBehaviour {

    public string mID = "Player1";
    private string mJob = "";
    public GameObject Texts;

    public void CardSelect(string job)
    {
        mJob = job;
        StartCoroutine(GetFile());
        Instantiate(Texts,GameObject.Find("Canvas").transform,false);
    }

    public void Login(int id)
    {
        mID = "Player"+(id+1);
    }

    IEnumerator GetFile()
    {

        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/"+mID+"DataSave.php?class=" + mJob);
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
}
