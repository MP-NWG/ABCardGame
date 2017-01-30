using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace GameMain
{
    public class SelectCard : MonoBehaviour, IPointerDownHandler
    {
        CardInfo info = new CardInfo();

        //カードの情報を書き出す
        private void Start()
        {
            string sideName = transform.parent.tag;
            info.card = gameObject;
            info.job  = (JobClass  )System.Enum.Parse(typeof(JobClass)  , gameObject.name);
            info.side = (PlayerSide)System.Enum.Parse(typeof(PlayerSide), sideName);
        }

        //カードが選択された場合
        public void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.MoveCardBattlePlace(info);
        }
    }
}