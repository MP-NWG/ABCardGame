using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameResult : MonoBehaviour
{
    [SerializeField, Tooltip("説明文")]
    Text text;

    [SerializeField, Tooltip("Win")]
    GameObject _Win;
    [SerializeField, Tooltip("Lose")]
    GameObject _Lose;


    [SerializeField, Tooltip("BGM")]
    AudioSource _audio;
    public AudioClip _WinMusic;
    public AudioClip _LoseMusic;
    void Awake()
    {
        
    }
    
    void Start()
    {
        int num = PlayerData.Instance.point;
        if(num >= 3)
        {
            //勝ち
            text.text = "Win";
            iTween.RotateTo(_Win, iTween.Hash(
             "y", 360 * 5,
             "time", 6
         ));
            _audio.PlayOneShot(_WinMusic);
        }
        else
        {
            //負け
            text.text = "Lose";
            iTween.RotateTo(_Lose, iTween.Hash(
             "y", 360 * 5,
             "time", 6
         ));
            _audio.PlayOneShot(_LoseMusic);
        }
        
    }
    
    void Update()
    {
        
    }
}