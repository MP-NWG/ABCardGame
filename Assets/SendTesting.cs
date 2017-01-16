using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
 
class SendTesting : MonoBehaviour {
    void Start()
    {
        StartCoroutine(Upload());
    }
 
    IEnumerator Upload()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:8888/UnityNetWorking.php");
        // 下記でも可
        // UnityWebRequest request = new UnityWebRequest("http://example.com");
        // methodプロパティにメソッドを渡すことで任意のメソッドを利用できるようになった
        // request.method = UnityWebRequest.kHttpVerbGET;
 
        // リクエスト送信
        yield return request.Send();
 
        // 通信エラーチェック
        if (request.isError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                // UTF8文字列として取得する
                string text = request.downloadHandler.text;
 
                // バイナリデータとして取得する
                byte[] results = request.downloadHandler.data;

                Debug.Log(text + ":" + results);
            }
        }
    }
}