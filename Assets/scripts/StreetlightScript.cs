using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetlightScript : MonoBehaviour
{
    public int timeToSwitch;
    private float timer;
    private int randValue;
    private MeshRenderer mesh;

    // Use this for initialization
    void Start()
    {
        timer = Time.time;
        mesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (Time.time - timer >= timeToSwitch)
        {
            if (mesh.enabled)
            {
                mesh.enabled = false;
            }
            else
            {
                mesh.enabled = true;
            }

            timer = Time.time;
        }
    }
}