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

    public void CraftItem(string itemID)
    {
        ItemData itemToCraft = GetCraftableItem(itemID);
        Debug.Log("Checking item...");

        if(itemToCraft != null)
        {
            Debug.Log("Starting to craft...");
            for (int i = 0; i < itemToCraft.materials.Length; i++)
            {
                if (!CraftingCore.CompareItemsWithInventory(itemToCraft.materials[i].id, itemToCraft.materials[i].count))
                {
                    Debug.Log("Not enough items.");
                    return;
                }
            }

            // --- ABLE TO CRAFT --- //
            CraftingCore.DeleteAllFromList();
            inventorySystem.AddItemViaName(itemToCraft.name);
        }

    }

    void StoreAllCraftableItems()
    {
        for (int i = 0; i < inventorySystem.items.Length; i++)
        {
            if (inventorySystem.items[i].craftable)
                craftableItems.Add(inventorySystem.items[i]);
        }
    }

    ItemData GetCraftableItem(string itemID)
    {
        for (int i = 0; i < craftableItems.Count; i++)
        {
            if (craftableItems[i].id == itemID)
                return craftableItems[i];
        }
        return null;
    }

}
