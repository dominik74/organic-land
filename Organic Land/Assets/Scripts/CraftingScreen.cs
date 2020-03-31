using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScreen : MonoBehaviour {

    public Transform craftingSlotsParent;
    public GameObject itemWindow;

    private InventorySystem inventorySystem;
    private CraftingSystem craftingSystem;

    private string selectedItemID;

    public static CraftingScreen instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventorySystem = InventorySystem.instance;
        craftingSystem = CraftingSystem.instance;
        InitializeCraftingSlots();
    }

    public void DisplayItem(GameObject targetItem)
    {
        itemWindow.SetActive(true);
        itemWindow.GetComponent<ItemWindow>().UpdateWindow(targetItem);
        selectedItemID = targetItem.GetComponent<Item>().id;
    }

    public void CraftSelectedItem()
    {
        if (selectedItemID != null)
            craftingSystem.CraftItem(selectedItemID);
    }

    void InitializeCraftingSlots()
    {
        for (int i = 0; i < craftingSystem.craftableItems.Count; i++)
        {
            GameObject newItem = inventorySystem.GetItem(craftingSystem.craftableItems[i].name);
            newItem.transform.SetParent(craftingSlotsParent.GetChild(i));
            newItem.transform.localScale = new Vector3(1.25f, 1.25f, 0);
            newItem.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
