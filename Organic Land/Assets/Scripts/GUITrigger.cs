using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITrigger : MonoBehaviour, IObjectController {

    public string guiName;

    public void Interact()
    {
        UIController.instance.SetScreen(guiName, true);
        Debug.Log("Opening GUI...");
    }
}
