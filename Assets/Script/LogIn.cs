using UnityEngine;
using System.Collections;

public class LogIn : MonoBehaviour {

    public GameManager mManager;

    void Login(string id)
    {
        mManager.mID = id;
        Instantiate(mManager);
    }
}
