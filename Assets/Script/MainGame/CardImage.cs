using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class CardImage : MonoBehaviour
{
    [Header("カードの表・裏")]
    [SerializeField, Tooltip("カードの表")]
    Sprite face;

    [SerializeField, Tooltip("カードの裏")]
    Sprite back;

    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        //スケールによってカードの表裏を判別する
        bool isFront = transform.localScale.x >= 0;

        image.sprite = (isFront) ? face : back;
    }
}