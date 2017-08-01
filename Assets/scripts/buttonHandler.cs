using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public PlayerController player;
    public GameObject exitObject;
    public GameObject objectActions;
    public objectsState objectContainer;
    public GameObject mainCamera;

    void Start()
    {
        player.ObjectInteraction += InsideObject;
        player.EnteredPhantomMode += PhantomMode;
    }

    public void EnterPhantomMode()
    {
        player.phantomMode = true;
        objectContainer.phantomMode = true;
        mainCamera.gameObject.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>().enabled = true;
    }

    public void ExitPhantomMode()
    {
        player.phantomMode = false;
        objectContainer.phantomMode = false;
        mainCamera.gameObject.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves>().enabled = false;
    }

    public void ActivateAbility()
    {
        player.usable.ActiveAbility();
    }

    public void ExitObject()
    {
        player.usable.LeaveObject();
    }

    public void InsideObject()
    {
        if (player.insideObject&&player.usable.HasAbility())
        {
            objectActions.SetActive(true);
            ExitPhantomMode();
        }
        else
        {
            objectActions.SetActive(false);
        }
    }

    public void PhantomMode()
    {
        if(player.phantomMode)
        {
            EnterPhantomMode();
        }
        else
        {
            ExitPhantomMode();
        }
    }
}
