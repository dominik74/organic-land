using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	Vector3[] vertices;

	private List<GameObject> myObjects = new List<GameObject>();

	void OnEnable()
	{
		//GenerateObjects();
	}

	void OnDisable()
	{
		ClearObjectList();
	}

	public void Generate()
	{
		GenerateObjects();
	}

	void GenerateObjects()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;

		for (int v = 0; v < vertices.Length; v++)
		{
			if (Random.Range(0, 51) == 1)
			{
				SpawnObject(vertices[v]);
			}
		}
	}

	void SpawnObject(Vector3 vertPos)
	{
		GameObject obj = ObjectPool.GetObject(TerrainGenerator.instance.GetRandomObjectData().name);
		if (obj != null)
		{
			obj.transform.SetParent(transform);
			obj.transform.localPosition = vertPos;
			obj.SetActive(true);

			myObjects.Add(obj);

			//InitializeObject(obj, TerrainGenerator.instance.GetRandomObjectData());
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

	void ClearObjectList()
	{
		for (int i = 0; i < myObjects.Count; i++)
		{
			if (myObjects[i] != null)
				myObjects[i].SetActive(false);
		}

		myObjects.Clear();
	}

}
