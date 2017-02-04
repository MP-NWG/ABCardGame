using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameMain;

public class BattleEnd : MonoBehaviour
{
    [SerializeField, Tooltip("説明文")]
    SceneChanger sceneChanger;


    void Awake()
    {
        PlayerData.Instance.battleNum++;
        if(PlayerData.Instance.battleNum >= 5)
        {
            //5試合終了
            Debug.Log("Battle Ended");
            sceneChanger.SceneChange("GameResult");
        }
        else
        {
            PlayerSide side = PlayerData.Instance.side;

            switch(side)
            {
                case PlayerSide.Emperor: side = PlayerSide.Slave;   break;
                case PlayerSide.Slave:   side = PlayerSide.Emperor; break;
            }

            PlayerData.Instance.side = side;

            sceneChanger.SceneChange("main");
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}