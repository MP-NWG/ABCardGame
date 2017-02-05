using UnityEngine;
using System.Collections;

public class TitleCard : MonoBehaviour {

    public float _Speed  = 0;
    private float y = 0;
    void Start () {
        y = transform.localPosition.y;
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 2, 0));
        y += -_Speed * Time.deltaTime;
        if (transform.localPosition.y <= -729)
        {
            y = 0;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);

    }
}
