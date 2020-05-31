using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureGenerator : MonoBehaviour {

	void Start()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject obj = TerrainGenerator.instance.objectTemplate;
			ObjectData data = TerrainGenerator.instance.GetObjectDataViaName(transform.GetChild(i).name);

			InitializeObject(obj, data);

			//obj.transform.SetParent(transform);
		}
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
			obj.AddComponent<Storage>().GenerateLootTable = true;
	}

}
