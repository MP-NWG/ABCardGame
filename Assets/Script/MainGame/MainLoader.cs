using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    public class MainLoader : MonoBehaviour
    {
        [SerializeField, Tooltip("説明文")]
        public PlayerSide job;

        [SerializeField]
        GameObject board;
    
        void Start()
        {
            Vector3 scale = board.transform.localScale;

            switch(job)
            {
                case PlayerSide.Emperor: scale.y =  1; break;
                case PlayerSide.Slave:   scale.y = -1; break;
            }

            board.transform.localScale = scale;
        }
    }
}