using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleEnd : MonoBehaviour
{
    //[SerializeField, Tooltip("説明文")]
    
    void Awake()
    {
        PlayerData.Instance.battleNum++;
        if(PlayerData.Instance.battleNum >= 5)
        {
            //5試合終了
            Debug.Log("Battle Ended");
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}