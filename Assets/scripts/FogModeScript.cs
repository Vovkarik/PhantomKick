using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogModeScript : MonoBehaviour {

    public Material transparentMat;
    public Material mat;
    private bool isFogMode;

    // Use this for initialization
    void Start () {
        isFogMode = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isFogMode)
            {
                gameObject.GetComponent<Renderer>().material = transparentMat;
                isFogMode = false;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = mat;
                isFogMode = true;
            }
        }
    }
}
