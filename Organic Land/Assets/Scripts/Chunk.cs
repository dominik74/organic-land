using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public Color primaryColor;
	public Color secondaryColor;
	
	public Color biome2ColorPrimary;
	public Color biome2ColorSecondary;
	
	public float biomeBlend = 0.5f;
	public float biomeOffset;
	public float biomeDetailScale = 256f;

	public bool dbg_biome1;
	public bool dbg_biome2;

	Vector3[] vertices;
	Color[] colors;

	private List<GameObject> myObjects = new List<GameObject>();

	void OnEnable()
	{
		//GenerateObjects();
		dbg_biome1 = false;
		dbg_biome2 = false;
	}

	void OnDisable()
	{
		ClearObjectList();
	}

	public void Generate()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		colors = new Color[vertices.Length];

		for (int v = 0; v < vertices.Length; v++)
		{
			Color currentColor;

			float biomeMap = Mathf.PerlinNoise((vertices[v].x + (transform.position.x / transform.localScale.x) + biomeOffset) / biomeDetailScale,
				(vertices[v].z + (transform.position.z / transform.localScale.z) + biomeOffset) / biomeDetailScale);

			if (biomeMap < 0.5f)
			{
				currentColor = Color.Lerp(primaryColor, secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
				dbg_biome1 = true;
			}
			else if (biomeMap <= 0.5f + biomeBlend)
			{
				Color biome1Color = Color.Lerp(primaryColor, secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
				Color biome2Color = Color.Lerp(biome2ColorPrimary, biome2ColorSecondary, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));

				currentColor = Color.Lerp(biome1Color, biome2Color, Mathf.Abs(biomeMap - 0.5f) / biomeBlend);
			}
			else
			{
				currentColor = Color.Lerp(biome2ColorPrimary, biome2ColorSecondary, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
				dbg_biome2 = true;
			}

			colors[v] = currentColor;

			if (v % 5 == 0)
				continue;

			GenerateObjects(v, vertices[v]);
		}

		mesh.colors = colors;
	}

	void GenerateObjects(int vertIndex, Vector3 vertPos)
	{
		GenerationController gc = GenerationController.instance;
		/*if(Random.Range(0, 41) == 1)
			SpawnObject(vertices[v]);*/

		if (Mathf.PerlinNoise(vertPos.x + gc.offsetX / gc.smoothness,
			vertPos.z + gc.offsetZ / gc.smoothness) >= gc.minimumHeight)
		{
			float chunkOffset = 0;
			float vertexOffset = 0;
			if (transform.position.x % 80 == 0 && transform.position.z % 80 == 0)
				chunkOffset = -5f;
			if (vertIndex % 2 == 0)
				vertexOffset = 2f;
			SpawnObject(vertices[vertIndex] + new Vector3(chunkOffset + vertexOffset, 0, chunkOffset + vertexOffset));
		}
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
