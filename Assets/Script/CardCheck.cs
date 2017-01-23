using UnityEngine;
using System.Collections;
// 追加
using UnityEngine.Networking;

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using UnityEngine.UI;

public class CardCheck : MonoBehaviour
{

    Text mText;

    public void Start()
    {
        mText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GetTest());
    }

    IEnumerator GetTest()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/ABCardGame/"+gameObject.name+"DataGet.php");
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
                mText.text = test;
                Debug.Log("test:"+test);
            }
        }
    }
}
