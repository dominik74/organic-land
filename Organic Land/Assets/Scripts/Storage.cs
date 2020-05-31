using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

	public static Storage current;

	private bool generateLootTable;
	public bool GenerateLootTable 
	{ 
		get { return generateLootTable; }
		set
		{
			generateLootTable = value;

			if (value)
			{
				LootSystem.instance.FillStorageWithLootTable(this);
			}
		}
	}

	private List<StoredItem> storedItems = new List<StoredItem>();
	private Transform storageSlotsParent;

	void Start()
	{
		storageSlotsParent = InventoryScreen.instance.storageView.GetChild(2);
	}

	public void Open()
	{
		current = this;
	}

	public void AddItem(string itemName, int index)
	{
		if (storageSlotsParent.GetChild(index).childCount == 0)
			storedItems.Add(new StoredItem(itemName, index));
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

	public void LoadStorage()
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
