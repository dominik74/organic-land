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

    public void DropItem(string itemToDrop, Vector3 targetPos, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            // Get ItemData
            ItemData itemData = InventorySystem.instance.GetItemData(itemToDrop);

            // Instantiate
            GameObject droppedItem = Instantiate(objectTemplate);

            // Initialize
            droppedItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.icon;

            Vector3 randomPos = new Vector3(Random.Range(-0.85f, 0.85f), 0, Random.Range(-0.85f, 0.85f));
            targetPos += randomPos;

            droppedItem.transform.position = targetPos;
            droppedItem.name = itemData.name;

            // Add components
            droppedItem.AddComponent<Pickable>();
        }
        
    }

}
