using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    public class MainLoader : MonoBehaviour
    {
        [SerializeField]
        GameObject board;
    
        void Start()
        {
            Vector3 scale = board.transform.localScale;

            int        id   = PlayerPrefs.GetInt("Playerside");
            PlayerSide side = (PlayerSide)System.Enum.ToObject(typeof(PlayerSide), id);

            switch(side)
            {
                case PlayerSide.Emperor: scale.y =  1; break;
                case PlayerSide.Slave:   scale.y = -1; break;
            }

            board.transform.localScale = scale;
        }
    }
}