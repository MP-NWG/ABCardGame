using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    [RequireComponent(typeof(Image))]
    public class PlayerSelectArea : MonoBehaviour
    {
        //[SerializeField, Tooltip("説明文")]
        public bool cardSelect = true;

        Image image;

        void Awake()
        {
            cardSelect = true;
            image = GetComponent<Image>();
            image.raycastTarget = false;
        }

        void Update()
        {
            //カード確定後にカードを移動できなくする
            image.raycastTarget = (cardSelect) ? false : true;
        }
    }
}