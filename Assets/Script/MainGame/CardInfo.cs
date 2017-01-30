using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    /// <summary> カードの情報 </summary>
    [System.Serializable]
    public class CardInfo
    {
        //カードのオブジェクト
        public GameObject card;
        //皇帝・奴隷 どちら側か
        public PlayerSide side;
        //カードの種類
        public JobClass job;

        /// <summary> 文字列に変換する </summary>
        /// <returns> カードの情報(文字列) </returns>
        public override string ToString()
        {
            return card.name       + "\n" +
                   side.ToString() + "\n" +
                   job.ToString();
        }
    }
}