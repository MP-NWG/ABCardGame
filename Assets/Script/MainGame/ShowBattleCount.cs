using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace GameMain
{
    public class ShowBattleCount : MonoBehaviour
    {
        [SerializeField, Tooltip("説明文")]
        Text text;
        
        void Awake()
        {
            
        }
    
        void Start()
        {
            int count = PlayerData.Instance.battleNum + 1;
            text.text = count.ToString() + "試合目";
        }
    
        void Update()
        {
        
        }
    }
}