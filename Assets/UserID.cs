using UnityEngine;
using System.Collections;

public class UserID : MonoBehaviour
{
    //
    public LogIn Login_script;
    //
    public int mPlayernun;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Login_script = GameObject.Find("LoginManager").GetComponent<LogIn>();
       
    }

    // Update is called once per frame
    void Update()    {          }

    public void getPlayerNum()
    {
        //
        mPlayernun = Login_script.Playernum;
    }
}