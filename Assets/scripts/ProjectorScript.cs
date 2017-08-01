using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScript : MonoBehaviour {

    public float angle;
    public int direction;
    public int speed;
    private bool changed;
    private float newAngle;

	void Start ()
    {
        changed = false;
    }
	
	void Update ()
    {
        Rotate();
        if (Math.Abs(transform.rotation.eulerAngles.z - 180) <= angle)
        {
            changed = false;
        }
        else if (!changed)
        {
            direction *= -1;
            changed = true;
        }
    }

    void Rotate()
    {
        newAngle = (transform.rotation.eulerAngles.z) + direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newAngle));
    }
}
