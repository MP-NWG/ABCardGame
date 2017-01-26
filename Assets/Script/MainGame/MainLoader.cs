using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainLoader : MonoBehaviour
{
    enum Job
    {
        Emperor,
        Slave
    }

    [SerializeField, Tooltip("説明文")]
    Job job;

    [SerializeField]
    GameObject board;
    
    void Start()
    {
        Vector3 scale = board.transform.localScale;

        switch(job)
        {
            case Job.Emperor: scale.y =  1; break;
            case Job.Slave:   scale.y = -1; break;
        }

        board.transform.localScale = scale;
    }
}