using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingSystem : MonoBehaviour {

    [HideInInspector]
    public List<ItemData> smeltableItems = new List<ItemData>();

    private InventorySystem inventorySystem;

    public static SmeltingSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventorySystem = InventorySystem.instance;
        StoreAllSmeltableItems();
    }

    public void SmeltItem(string itemID)
    {
        ItemData itemToSmelt = GetSmeltableItem(itemID);

        if (itemToSmelt != null)
        {
            for (int i = 0; i < itemToSmelt.materials.Length; i++)
            {
                if (!CraftingCore.CompareItemsWithInventory(itemToSmelt.materials[i].id, itemToSmelt.materials[i].count))
                {
                    Debug.Log("Not enough items.");
                    return;
                }
            }

            // --- ABLE TO SMELT --- //
            CraftingCore.DeleteAllFromList();
            inventorySystem.AddItemViaName(itemToSmelt.name);
        }
    }

    void StoreAllSmeltableItems()
    {
        for (int i = 0; i < inventorySystem.items.Length; i++)
        {
            if (inventorySystem.items[i].smeltable)
                smeltableItems.Add(inventorySystem.items[i]);
        }
    }

    ItemData GetSmeltableItem(string itemID)
    {
        for (int i = 0; i < smeltableItems.Count; i++)
        {
            if (smeltableItems[i].id == itemID)
                return smeltableItems[i];
        }
        return null;
    }

}
