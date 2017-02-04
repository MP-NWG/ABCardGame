using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class PlayerData : SingletonMonoBehaviourFast<PlayerData>
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //プレイヤーの情報
    public GameMain.PlayerSide side;

    public int point = 0;
    
    public int battleNum = 0;
}