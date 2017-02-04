using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerSideConfirm : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, Tooltip("説明文")]
    GameMain.PlayerSide side;

    [SerializeField]
    SceneChanger changer;

    [SerializeField]
    AudioClip clip;

    AudioSource source;

    void Awake()
    {
        source = new AudioSource();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerData.Instance.side      = side;
        PlayerData.Instance.point     = 0;
        PlayerData.Instance.battleNum = 0;
        source.PlayOneShot(clip);
        changer.SceneChange();
    }
}