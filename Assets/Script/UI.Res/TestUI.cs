using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.RotateTo(transform.gameObject, iTween.Hash(
             "y", 360 * 5,
             "time", 6
         ));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
