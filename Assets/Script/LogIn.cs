using UnityEngine;
using System.Collections;

public class LogIn : MonoBehaviour
{

    public GameManager mManager;
    public string id;

    void Login(string id)
    {
        mManager.mID = id;
        Instantiate(mManager);
    }

    //プレイヤー１か２判別
    public void Plyer1()
    {
        id = "Plyer1";
    }

    public void Plyer2()
    {
        id = "Plyer2";
    }


    void stertLogin()
    {
        

    }

    void update()
    {//確認
       print(id);
    }
}
