using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public Transform slotsParent;

    public ItemData[] items;

    public void AddItem(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].name == name)
            {
                // TODO: Implement
                Debug.Log("Added item");
            }
        }
    }

}
