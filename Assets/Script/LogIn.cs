using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class LogIn : SingletonMonoBehaviourFast<LogIn>//, IPointerDownHandler
{
    UserID muserID;
    //カギ
    //string Sidekey = "Playerside";
    public int Playernum;

    void Start()
    {
        muserID = GameObject.Find("UserIdData").GetComponent<UserID>();
    }
    void Login(string id)
    {
        //mManager.mID = id;
        //Instantiate(mManager);
    }

    //プレイヤー１Button
    public void Plyer1()
    {
        Playernum = 1;
        muserID.getPlayerNum();
        SceneManager.LoadScene("main");
    }

    //プレイヤー2ボタン
    public void Plyer2()
    {
        Playernum = 2;
        muserID.getPlayerNum();
        SceneManager.LoadScene("main");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name);
    }

    //デバッグ用
    public void debugOn()
    {
        //int usedebug = PlayerPrefs.GetInt(Sidekey);
        //Debug.Log(usedebug);
    }
}