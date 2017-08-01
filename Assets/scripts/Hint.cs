using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hint : MonoBehaviour {

    public GameObject hintPanel;
    public GameObject nextPoint;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "player" || coll.gameObject.name == "usable")
        {
            hintPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "player" || coll.gameObject.name == "usable")
        {
            hintPanel.SetActive(false);
            Destroy(gameObject);
            nextPoint.SetActive(true);
        }
    }
}
