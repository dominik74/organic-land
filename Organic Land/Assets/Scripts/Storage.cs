using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

	public static Storage current;

	private List<StoredItem> storedItems = new List<StoredItem>();
	private Transform storageSlotsParent;

	void Awake()
	{
		storageSlotsParent = InventoryScreen.instance.storageView.GetChild(2);
	}

	public void Open()
	{
		current = this;
		LoadStorage();
	}

	public void GenerateLootTable()
	{
		LootTable lootTable = LootSystem.instance.GetStorageLootTable();
		List<int> rndIndexes = new List<int>();

		for (int i = 0; i < lootTable.materials.Length; i++)
		{
			for (int y = 0; y < lootTable.materials[i].count; y++)
			{
				int random = Random.Range(0, 27);

				if(!rndIndexes.Contains(random))
				{
					storedItems.Add(new StoredItem(lootTable.materials[i].name, random));
					rndIndexes.Add(random);
				}
			}
		}

	}

	public void SaveStorage()
	{
		storedItems.Clear();
		for (int i = 0; i < storageSlotsParent.childCount; i++)
		{
			if (storageSlotsParent.GetChild(i).childCount != 0)
			{
				storedItems.Add(new StoredItem(storageSlotsParent.GetChild(i).GetChild(0).name, i));
			}
		}
	}

	void LoadStorage()
	{
		ClearStorage();
		for (int i = 0; i < storedItems.Count; i++)
		{
			GameObject newItem = InventorySystem.instance.GetItem(storedItems[i].itemName);
			newItem.transform.SetParent(storageSlotsParent.GetChild(storedItems[i].index));
			newItem.transform.localScale = new Vector3(1.25f, 1.25f, 0);
			newItem.transform.localPosition = new Vector3(0, 0, 0);
		}
	}

	void ClearStorage()
	{
		for (int i = 0; i < storageSlotsParent.childCount; i++)
		{
			if (storageSlotsParent.GetChild(i).childCount != 0)
				Destroy(storageSlotsParent.GetChild(i).GetChild(0).gameObject);
		}
	}

	struct StoredItem
	{
		public string itemName;
		public int index;

		public StoredItem(string itemName, int index)
		{
			this.itemName = itemName;
			this.index = index;
		}
	}
}
