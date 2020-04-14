using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCore : MonoBehaviour {

    static List<GameObject> itemsToRemove = new List<GameObject>();

    public static bool CompareItemsWithInventory(string itemID, int minItemCount)
    {
        GameObject[] availableItems = InventoryScreen.instance.FindItems(itemID, minItemCount);
        itemsToRemove.AddRange(availableItems);

        return availableItems.Length == minItemCount;
    }

    public static void DeleteAllFromList()
    {
        for (int i = 0; i < itemsToRemove.Count; i++)
        {
            Destroy(itemsToRemove[i]);
        }
        itemsToRemove.Clear();
    }

}
