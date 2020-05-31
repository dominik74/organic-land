using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour {

    public GameObject objectTemplate;

    public LootTable storageLootTable;

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
            ItemData itemData = InventorySystem.instance.GetItemDataID(itemToDrop);

            // Instantiate
            GameObject droppedItem = Instantiate(objectTemplate);

            // Initialize
            droppedItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemData.icon;

            Vector3 randomPos = new Vector3(Random.Range(-1.25f, 1.25f), 0, Random.Range(-1.25f, 1.25f));
            targetPos += randomPos;

            droppedItem.transform.position = targetPos;
            droppedItem.name = itemData.name;

            // Add components
            droppedItem.AddComponent<Pickable>();
        }
        
    }

    public void DropLootTable(LootTable lootTable, Vector3 targetPos)
    {
        for (int i = 0; i < lootTable.materials.Length; i++)
        {
            for (int y = 0; y < lootTable.materials[i].count; y++)
            {
                DropItem(lootTable.materials[i].name, targetPos);
            }
        }
    }

    public void FillStorageWithLootTable(Storage storage)
    {
        for (int i = 0; i < storageLootTable.materials.Length; i++)
        {
            for (int y = 0; y < storageLootTable.materials[i].count; y++)
            {
                storage.AddItem(storageLootTable.materials[i].name, Random.Range(0, 21));
            }
        }
    }

}

[System.Serializable]
public struct LootTable
{
    public LootTableMaterial[] materials;
}

[System.Serializable]
public struct LootTableMaterial
{
    public string name;
    public int count;
}
