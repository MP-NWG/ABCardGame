using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultShower : MonoBehaviour
{
    [SerializeField, Tooltip("勝利時のパーティクルやアニメーション")]
    GameObject showWinObject;

    [SerializeField, Tooltip("敗北時のパーティクルやアニメーション")]
    GameObject showLoseObject;

    [SerializeField, Tooltip("引き分け時のパーティクルやアニメーション")]
    GameObject showDrawObject;

    public void Show(bool? battleResult)
    {
        if(battleResult.HasValue)
        {
            if(battleResult.Value == true)
            {
                Instantiate(showWinObject);
            }
            else
            {
                Instantiate(showLoseObject);
            }
        }
        else
        {
            Instantiate(showDrawObject);
        }
    }
}