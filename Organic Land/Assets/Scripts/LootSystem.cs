using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour {

    public GameObject objectTemplate;

    public static LootSystem instance;
    private void Awake()
    {
        instance = this;
    }

    public void DropItem(string itemToDrop, Vector3 targetPos)
    {
        // Get ItemData
        ItemData itemData = InventorySystem.instance.GetItemData(itemToDrop);

        // Instantiate
        GameObject droppedItem = Instantiate(objectTemplate);

        // Initialize
        droppedItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.icon;
        droppedItem.transform.position = targetPos;
        droppedItem.name = itemData.name;

        // Add components
        droppedItem.AddComponent<Pickable>();
    }

}
