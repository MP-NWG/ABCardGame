using UnityEngine;
using System.Collections;

public class UserID : MonoBehaviour
{
   public int mPlayernun;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()    {          }

    public void getPlayerNum()
    {
        //mPlayernun = LogIn.Instance.Playernum;
    }
}