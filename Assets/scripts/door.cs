using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {
    public GameObject frontPlane;
    public staticCamera cam;
    private SpriteRenderer sprite;
    private bool insideBuilding = false;
	// Use this for initialization
	void Start () {
        sprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!insideBuilding)
            {
                frontPlane.SetActive(false);
                //sprite.enabled = false;
                insideBuilding = true;
                cam.ChangeCameraPos(1);
            }
            else
            {
                frontPlane.SetActive(true);
                //sprite.enabled = true;
                insideBuilding = false;
                cam.ChangeCameraPos(-1);
            }
        }
    }
}
