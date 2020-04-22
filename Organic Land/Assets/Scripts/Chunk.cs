using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	Vector3[] vertices;

	void OnEnable()
	{
		GenerateObjects();
	}

	void GenerateObjects()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;

		for (int v = 0; v < vertices.Length; v++)
		{
			if (Random.Range(0, 31) == 1)
			{
				SpawnObject(vertices[v]);
			}
		}
	}

	void SpawnObject(Vector3 vertPos)
	{
		GameObject obj = Instantiate(TerrainGenerator.instance.objectTemplate);
		obj.transform.SetParent(transform);
		obj.transform.position = new Vector3(vertPos.x * transform.localScale.x, vertPos.y, vertPos.z * transform.localScale.z);

		InitializeObject(obj, TerrainGenerator.instance.GetRandomObjectData());
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
