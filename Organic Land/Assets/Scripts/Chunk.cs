using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public Color primaryColor;
	public Color secondaryColor;

	Vector3[] vertices;
	Color[] colors;

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
		colors = new Color[vertices.Length];

		GenerationController gc = GenerationController.instance;
		Color currentColor = secondaryColor;

		for (int v = 0; v < vertices.Length; v++)
		{
			/*if (Mathf.PerlinNoise(vertices[v].x + 4.59f + transform.position.x / 15, vertices[v].z + 4.59f + transform.position.z / 15) >= 0.48f)
			{
				currentColor = secondaryColor;
			}
			else
			{
				currentColor = primaryColor;
			}*/
			
			currentColor = Color.Lerp(primaryColor, secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
			
			/*if (v % 3 == 0)
			{
				currentColor = Color.Lerp(primaryColor, secondaryColor, Random.Range(0, 1f));
			}*/
			colors[v] = currentColor;

			if (v % 5 == 0)
				continue;

			Vector3 vertPos = vertices[v];

			/*if(Random.Range(0, 41) == 1)
				SpawnObject(vertices[v]);*/

			if (Mathf.PerlinNoise(vertPos.x + gc.offsetX / gc.smoothness,
				vertPos.z + gc.offsetZ / gc.smoothness) >= gc.minimumHeight)
			{
				float chunkOffset = 0;
				float vertexOffset = 0;
				if (transform.position.x % 80 == 0 && transform.position.z % 80 == 0)
					chunkOffset = -5f;
				if (v % 2 == 0)
					vertexOffset = 2f;
				SpawnObject(vertices[v] + new Vector3(chunkOffset + vertexOffset, 0, chunkOffset + vertexOffset));
			}
		}

		mesh.colors = colors;
	}

	void SpawnObject(Vector3 pos)
	{
		GameObject obj = ObjectPool.GetObject(TerrainGenerator.instance.GetRandomObjectData().name);
		if (obj != null)
		{
			obj.transform.SetParent(transform);
			obj.transform.localPosition = pos;
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
