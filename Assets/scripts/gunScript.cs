using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {
    public GameObject gunEnd;
    public GameObject bulletPref;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Shoot();
        }
	}
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, gunEnd.transform.position, gunEnd.transform.rotation);
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }
}
