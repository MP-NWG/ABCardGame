using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    public class Send : MonoBehaviour, IPointerDownHandler
    {
        //[SerializeField, Tooltip("説明文")]
        

        public void OnPointerDown(PointerEventData eventData)
        {
            //通信
            Debug.Log(transform.tag);
        }
    }
}