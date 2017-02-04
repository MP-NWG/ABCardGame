using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ReturnLogin : MonoBehaviour, IPointerDownHandler
{
    
    [SerializeField, Tooltip("説明文")]
    SceneChanger sceneChanger;

    void Awake()
    {
        
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        sceneChanger.SceneChange();
    }
}