﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITrigger : MonoBehaviour, IObjectController {

    public string guiName;
    public InventoryLayout invLayout;

    public void Interact()
    {
        UIController.instance.SetScreen(guiName, true);

        if(guiName == "pnlInventory")
            InventoryScreen.instance.InvLayout = invLayout;

        Storage storage = GetComponent<Storage>();
        if (storage != null)
            storage.Open();

        Debug.Log("Opening GUI...");
    }
}
