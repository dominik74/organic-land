using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingSystem : MonoBehaviour {

    [HideInInspector]
    public List<ItemData> smeltableItems = new List<ItemData>();

    public float smeltTime = 1f;

    private InventorySystem inventorySystem;

    private string currentSmeltingItem;
    private bool smelting;

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

        currentSmeltingItem = itemToSmelt.name;

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

            SmeltingScreen.instance.SetProgressbar(true);
            StopCoroutine("SmeltTimer");
            StartCoroutine("SmeltTimer");
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

    void OnFinishedSmelting()
    {
        SmeltingScreen.instance.SetProgressbar(false);
        inventorySystem.AddItemViaName(currentSmeltingItem);
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

    IEnumerator SmeltTimer()
    {
        smelting = true;
        float startSmeltTime = Time.unscaledTime;
        while ((Time.unscaledTime - startSmeltTime) < smeltTime)
        {
            SmeltingScreen.instance.UpdateProgressbar((Time.unscaledTime - startSmeltTime) / smeltTime);
            yield return null;
        }
        smelting = false;

        OnFinishedSmelting();
        Debug.Log("Done!");
    }

}
