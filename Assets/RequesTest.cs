using UnityEngine;
using System.Collections;
// 追加
using UnityEngine.Networking;

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

public class RequesTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetFile());
    }

    IEnumerator GetFile()
    {

        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/Save.php?class=emperor");
        yield return request.Send();

        // 何らかのエラーがあったら
        if (request.isError)
        {
            // エラー処理
            Debug.Log("エラー");
        }
        else {
            // レスポンスコードを見る
            if (request.responseCode == 200)
            {
                string test = request.downloadHandler.text;
                Debug.Log(test);
            }
        }
    }
}
