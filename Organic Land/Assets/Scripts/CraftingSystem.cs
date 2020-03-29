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

    public void CraftItem(string itemName)
    {
        ItemData itemToCraft = GetCraftableItem(itemName);
        Debug.Log("Checking item...");

        if(itemToCraft != null)
        {
            Debug.Log("Starting to craft...");
            List<GameObject> itemsToRemove = new List<GameObject>();
            for (int i = 0; i < itemToCraft.materials.Length; i++)
            {
                GameObject[] availableItems = InventoryScreen.instance.FindItems(itemToCraft.materials[i].name, itemToCraft.materials[i].count);
                Debug.Log(availableItems.Length);
                if (availableItems.Length == itemToCraft.materials[i].count)
                    itemsToRemove.AddRange(availableItems);
                else
                {
                    Debug.Log("Not enough items.");
                    return;
                }

            }
            // --- ABLE TO CRAFT --- //
            DeleteAllFromList(itemsToRemove);
            inventorySystem.AddItemViaName(itemToCraft.name);
            Debug.Log("success!");
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

    ItemData GetCraftableItem(string itemName)
    {
        for (int i = 0; i < craftableItems.Count; i++)
        {
            if (craftableItems[i].name == itemName)
                return craftableItems[i];
        }
        return null;
    }

    void DeleteAllFromList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
        list.Clear();
    }

}
