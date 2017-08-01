using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class lightCheck : MonoBehaviour {
    public buttonHandler bh;
    private Material mat;
    private bool throughLight = false;
    private LineRenderer parentLine;
    private Vector3 startPos;
    private Vector3 endPos;
    private EdgeCollider2D col;
    private Vector2[] points = new Vector2[2];

    void Start()
    {
        parentLine = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        points[0] = Vector2.zero;
        points[1] = Vector2.zero;
        startPos = transform.InverseTransformPoint(parentLine.GetPosition(0));
    }

    void Update()
    {
        if (throughLight)
        {
            if (ExitPM != null)
            {
                ExitPM();
            }
            Destroy(gameObject);
            throughLight = false;
        }
        endPos = transform.InverseTransformPoint(parentLine.GetPosition(1));
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        points[0] = startPos;
        points[1] = endPos;
        col.points = points;
        // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle - 90);
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Zachem nakurivatb osu");
        if (col.gameObject.tag == "lightSource")
        {
            Debug.Log("Zatem!");
            throughLight = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "lightSource")
        {
            throughLight = false;
            parentLine.startColor = Color.cyan;
        }
    }

    public bool CanFly()
    {
        return !throughLight;
    }
    public event Action ExitPM;

}
