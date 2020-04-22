using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	public GameObject objectTemplate;
	public GameObject tileTemplate;
	[Range(0, 500)]
	public int instanceCount;
	[Range(0, 500)]
	public int tileCount;

	public ObjectData[] objectData;

	private static List<GameObject> pooledObjects = new List<GameObject>();
	private static List<GameObject> tiles = new List<GameObject>();

	private int objCount;
	private int tileIntCount;

	void Awake()
	{
		objCount = 0;
		tileIntCount = 0;

		InstantiateObjects();
		InstantiateTiles();

		Debug.Log("INSTANTIATED OBJECTS: " + objCount);
		Debug.Log("INSTANTIATED TILES: " + tileIntCount);
	}

	public static GameObject GetObject(string objName)
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (pooledObjects[i].name == objName && !pooledObjects[i].activeSelf)
			{
				return pooledObjects[i];
			}
		}
		return null;
	}

	public static GameObject GetTile()
	{
		for (int i = 0; i < tiles.Count; i++)
		{
			if (!tiles[i].activeSelf)
			{
				return tiles[i];
			}
		}
		return null;
	}

	void InstantiateObjects()
	{
		for (int i = 0; i < objectData.Length; i++)
		{
			if (objectData[i].spawnChance >= 0)
			{
				for (int x = 0; x < instanceCount; x++)
				{
					GameObject obj = Instantiate(objectTemplate);
					obj.transform.position = new Vector3(0, 0, 0);
					obj.SetActive(false);

					InitializeObject(obj, objectData[i]);

					pooledObjects.Add(obj);
					objCount++;
				}
			}
		}
	}

	void InstantiateTiles()
	{
		for (int i = 0; i < tileCount; i++)
		{
			GameObject tile = Instantiate(tileTemplate);
			tile.transform.position = new Vector3(0, 0, 0);
			tile.SetActive(false);

			tiles.Add(tile);
			tileIntCount++;
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
	}

}
