using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameResult : MonoBehaviour
{
    [SerializeField, Tooltip("説明文")]
    Text text;

    void Awake()
    {
        
    }
    
    void Start()
    {
        int num = PlayerData.Instance.point;
        if(num >= 3)
        {
            //勝ち
            text.text = "Win";
        }
        else
        {
            //負け
            text.text = "Lose";
        }
        
    }
    
    void Update()
    {
        
    }
}