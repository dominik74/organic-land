using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = InventorySystem.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventorySystem.SelectSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            inventorySystem.SelectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            inventorySystem.SelectSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            inventorySystem.SelectSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            inventorySystem.SelectSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            inventorySystem.SelectSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            inventorySystem.SelectSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            inventorySystem.SelectSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            inventorySystem.SelectSlot(8);

        if (Input.GetKeyDown(KeyCode.X))
            inventorySystem.RemoveSelectedItem();
    }

}
