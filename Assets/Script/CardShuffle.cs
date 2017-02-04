using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardShuffle : MonoBehaviour
{
    void Awake()
    {
        for(int i = 0; i < 5; i++)
        {
            Shuffle();
        }

        for(int i = -2; i <= 2; i++)
        {
            Transform child = transform.GetChild(i + 2);
            Vector3 position = child.transform.localPosition;
            position.x = i * 100;
            child.transform.localPosition = position;
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void Shuffle()
    {
        foreach(Transform card in transform)
        {
            int rand = Random.Range(0, 2);

            if(rand == 0)
            {
                card.transform.SetAsFirstSibling();
            }
            else
            {
                card.transform.SetAsLastSibling();
            }
        }
    }
}