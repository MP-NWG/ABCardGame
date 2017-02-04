using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    public class MainLoader : MonoBehaviour
    {
        [SerializeField]
        GameObject board;

        [SerializeField]
        GameObject emperorCards;

        [SerializeField]
        GameObject slaveCards;
    
        void Start()
        {
            float boardRot = 0;            
            PlayerSide side = PlayerData.Instance.side;

            switch(side)
            {
                case PlayerSide.Emperor:
                    boardRot = 0;
                    SetCardBack(slaveCards.transform);
                    break;

                case PlayerSide.Slave:
                    boardRot = 180;
                    SetCardBack(emperorCards.transform);
                    break;
            }

            board.transform.Rotate(Vector3.forward, boardRot);
        }

        void SetCardBack(Transform cards)
        {
            foreach(Transform card in cards)
            {
                card.localScale = new Vector3(-1, 1, 0);
                card.Rotate(Vector3.forward, 180);
            }
        }
    }
}