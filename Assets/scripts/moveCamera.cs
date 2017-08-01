using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {
    public int direction;
    public staticCamera cam;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            cam.ChangeCameraPos(direction);
        }
        else if(col.gameObject.tag == "usable")
        {
            if(col.gameObject.GetComponent<IUsable>().IsPossessed())
            {
                cam.ChangeCameraPos(direction);
            }
        }
    }
}
