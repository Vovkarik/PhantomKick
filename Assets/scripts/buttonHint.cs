using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHint : MonoBehaviour {

    public GameObject prevHintPanel;
    public GameObject hintPanel;
    public PlayerController player;
    void Start()
    {
        player.ObjectInteraction += HideHint;
    }

    public void ShowHint()
    {
        if (hintPanel != null)
        {
            prevHintPanel.SetActive(false);
            hintPanel.SetActive(true);
        }
    }

    public void HideHint()
    {
        if (hintPanel != null && player.insideObject)
        {
            Destroy(hintPanel.gameObject);
        }
    }
}
