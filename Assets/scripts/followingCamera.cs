using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingCamera : MonoBehaviour {
    public Transform player;
    private Vector3 playerPos;
    private Vector3 defaultPos;
	
	void Start ()
    {
        defaultPos = transform.position;
	}
	
	void Update ()
    {
        playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, defaultPos.y, defaultPos.z);
	}
}
