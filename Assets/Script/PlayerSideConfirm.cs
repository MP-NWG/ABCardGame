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
        PlayerData.Instance.side = side;
        source.PlayOneShot(clip);
        changer.SceneChange();
    }
}