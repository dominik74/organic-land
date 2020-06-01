using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureGenerator : MonoBehaviour {

	private List<GameObject> objects = new List<GameObject>();

	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			string objName = transform.GetChild(i).name;

			if (objName == "Enemy")
			{
				MobPool.SpawnEnemy(transform.GetChild(i).position + new Vector3(0, 1, 0));
			}
			else
			{
				GameObject obj = Instantiate(TerrainGenerator.instance.objectTemplate);
				ObjectData data = TerrainGenerator.instance.GetObjectDataViaName(objName);

				InitializeObject(obj, data);

				obj.transform.position = transform.GetChild(i).position;
				objects.Add(obj);
			}
		}
		
		DeleteAllChildren();
		ParentNewObjects();	
	}

	void InitializeObject(GameObject obj, ObjectData data)
	{
		obj.name = data.name;
		obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = data.sprite;
		obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.color;

		if (data.collectBehavior == CollectBehavior.pickable)
			obj.AddComponent<Pickable>();
		else if (data.collectBehavior == CollectBehavior.minable)
		{
			obj.AddComponent<Minable>().collectTool = data.collectTool;
			obj.GetComponent<Minable>().lootTable = data.lootTable;
		}

		if (data.includeStorage)
		{
			GUITrigger guiTrigger = obj.AddComponent<GUITrigger>();
			guiTrigger.guiName = "pnlInventory";
			guiTrigger.invLayout = InventoryLayout.storage;
			
			obj.AddComponent<Storage>().GenerateLootTable();
		}
			
	}
	
	void DeleteAllChildren()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
	
	void ParentNewObjects()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].transform.SetParent(transform);
		}
	}

}
