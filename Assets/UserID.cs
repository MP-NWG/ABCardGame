using UnityEngine;
using System.Collections;

public class UserID : MonoBehaviour
{

    //
    int mPlayernun;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        mPlayernun = LogIn.Instance.Plyernum;
    }
}