﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    //public GameManager mManager;
    //public string id;

    //カギ
    string Sidekey = "Playerside";
    public int Plyernum;

    void Login(string id)
    {
        //mManager.mID = id;
        //Instantiate(mManager);
    }

    //プレイヤー１Button
    public void Plyer1()
    {
        Plyernum = 1;
        PlayerPrefs.SetInt(Sidekey, Plyernum);
        //Debug.Log(Plyernum);
    }
    //プレイヤー2ボタン
    public void Plyer2()
    {
        Plyernum = 2;
        PlayerPrefs.SetInt(Sidekey, Plyernum);
        //Debug.Log(Plyernum);
    }

    //デバッグ用
    public void debugOn()
    {
        int usedebug = PlayerPrefs.GetInt(Sidekey);
        Debug.Log(usedebug);
    }
}