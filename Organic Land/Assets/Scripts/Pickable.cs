using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public void Pickup()
    {
        InventorySystem.instance.AddItem(name.Replace(" (entity)", ""));
        Destroy(gameObject);
        HUDController.instance.DisplayObjectLabel("");
    }
}
