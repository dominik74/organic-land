using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour {

    [HideInInspector]
    public List<ItemData> craftableItems = new List<ItemData>();

    private InventorySystem inventorySystem;

    public static CraftingSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventorySystem = InventorySystem.instance;
        StoreAllCraftableItems();
    }

    void StoreAllCraftableItems()
    {
        for (int i = 0; i < inventorySystem.items.Length; i++)
        {
            if (inventorySystem.items[i].craftable)
                craftableItems.Add(inventorySystem.items[i]);
        }
    }

}
