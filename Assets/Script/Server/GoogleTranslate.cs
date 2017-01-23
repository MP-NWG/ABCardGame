// jetrans.cs
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

using UnityEngine;
using System.Collections;

public class GoogleTranslate : MonoBehaviour
{
    public void Start()
    {
        Main();
    }

    static void Main()
    {
        Encoding enc = Encoding.UTF8;

        string input = "私は普通のC#プログラマです。";

        string url = "http://translate.google.com/translate_t";
        string param = "";

        // ポスト・データの作成
        Hashtable ht = new Hashtable();

        ht["text"] = Uri.EscapeUriString(input);
        ht["langpair"] = "ja|en";
        ht["hl"] = "en";
        ht["ie"] = "UTF8";

        foreach (string k in ht.Keys)
        {
            param += String.Format("{0}={1}&", k, ht[k]);
        }
        byte[] data = Encoding.ASCII.GetBytes(param);

        // リクエストの作成
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.UseDefaultCredentials = true;
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = data.Length;

        // ポスト・データの書き込み
        Stream reqStream = req.GetRequestStream();
        reqStream.Write(data, 0, data.Length);
        reqStream.Close();

        // レスポンスの取得と読み込み
        WebResponse res = req.GetResponse();
        Stream resStream = res.GetResponseStream();
        StreamReader sr = new StreamReader(resStream, enc);
        string html = sr.ReadToEnd();
        sr.Close();
        resStream.Close();

        // 必要なデータの切り出し
        // 結果は「wrap=PHYSICAL>～</textarea>」にあるという前提
        string startmark = "wrap=PHYSICAL>";
        int start = html.IndexOf(startmark) + startmark.Length;
        int end = html.IndexOf("</textarea>", start);
        string result = html.Substring(start, end - start);

        Console.WriteLine(result);
        // 出力：I am the normal C# programmer.
    }
}
