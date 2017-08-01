using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticCamera : MonoBehaviour {

    public Transform player;
    public Vector3[] cameraPositions;
    private Vector3 playerPos;
    private Vector3 defaultPos;
    private int currentCamPos;
    private float delay;

    void Start()
    {
        defaultPos = transform.position;
        delay = Time.time;
    }

    void Update()
    {
        /*playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, defaultPos.y, defaultPos.z);*/
    }

    public void ChangeCameraPos(int amount)
    {
        if (Time.time - delay > 1)
        {
            currentCamPos += amount;
            SetCameraPos(currentCamPos);
        }
        
    }

    public void SetCameraPos(int pos)
    {
        if (pos >= 0)
        {
            transform.position = cameraPositions[pos];
            delay = Time.time;
        }
    }
}
