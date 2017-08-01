using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectsState : MonoBehaviour {

    public bool phantomMode
    {
        get { return isPhantom; }
        set
        {
            if (isPhantom == value) return;
            isPhantom = value;
            ChangeObjectsMode(isPhantom);
        }
    }

    private bool isPhantom;

	void Start ()
    {
        phantomMode = false;
	}

    private void ChangeObjectsMode(bool mode)
    {
        IUsable[] objects = gameObject.GetComponentsInChildren<IUsable>();
        foreach (IUsable a in objects)
        {
            a.Highlight(mode);
        }
    }
}
