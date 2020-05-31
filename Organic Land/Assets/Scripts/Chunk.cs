using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public Biome[] biomes;

	public Color primaryColor;
	public Color secondaryColor;
	
	public Color biome2ColorPrimary;
	public Color biome2ColorSecondary;
	
	public float biomeBlend = 0.5f;
	public float biomeOffset;
	public float biomeDetailScale = 256f;

	public bool usePerlinNoise = true;

	public GameObject structure;

	[HideInInspector] public string currentBiomeName;

	public bool dbg_biome1;
	public bool dbg_biome2;

	Vector3[] vertices;
	Color[] colors;

	private Biome currentBiome;

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

			GenerateColors(v);

			if (v % 5 == 0)
				continue;

			GenerateObjects(v);
		}

		currentBiomeName = currentBiome.name;
		mesh.colors = colors;
	}

	void GenerateColors(int v)
	{
		Color currentColor = Color.black;

        #region Biome Map Calculation
        float biomeNoise1 = Mathf.PerlinNoise((vertices[v].x + (transform.position.x / transform.localScale.x) + biomeOffset) / biomeDetailScale,
			(vertices[v].z + (transform.position.z / transform.localScale.z) + biomeOffset) / biomeDetailScale);

		float biomeNoise2 = Mathf.PerlinNoise(((vertices[v].x + (transform.position.x / transform.localScale.x) + 5.72f) / 30), 
			((vertices[v].z + (transform.position.z / transform.localScale.z) + 5.72f) / 30));

		float biomeMap = biomeNoise1 + 0.2f * biomeNoise2;
        #endregion

        for (int b = 0; b < biomes.Length; b++)
		{
			if (biomeMap <= biomes[b].height)
			{
				currentColor = Color.Lerp(biomes[b].primaryColor, biomes[b].secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
				dbg_biome1 = true;
				currentBiome = biomes[b];
				break;
			}
			else if (biomeMap <= biomes[b].height + biomeBlend)
			{
				Color biome1Color = Color.Lerp(biomes[b].primaryColor, biomes[b].secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));
				Color biome2Color = Color.Lerp(biomes[b + 1].primaryColor, biomes[b + 1].secondaryColor, Mathf.PerlinNoise(vertices[v].x * 2.2f, vertices[v].z * 2.2f));

				currentColor = Color.Lerp(biome1Color, biome2Color, Mathf.Abs(biomeMap - biomes[b].height) / biomeBlend);
				currentBiome = biomes[b + 1];
				break;
			}
		}



		/*if (biomeMap < 0.5f)
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
		}*/

		colors[v] = currentColor;
	}

	void GenerateObjects(int v)
	{		
		if (usePerlinNoise)
		{
			GenerationController gc = GenerationController.instance;

			if (Mathf.PerlinNoise(vertices[v].x + gc.offsetX / gc.smoothness,
				vertices[v].z + gc.offsetZ / gc.smoothness) >= gc.minimumHeight)
			{
				float chunkOffset = 0;
				float vertexOffset = 0;
				if ((transform.position.x / transform.localScale.x) % 80 == 0 && (transform.position.z / transform.localScale.z) % 80 == 0)
					chunkOffset = -5f;
				if (v % 2 == 0)
					vertexOffset = 2f;
				SpawnObject(vertices[v] + new Vector3(chunkOffset + vertexOffset, 0, chunkOffset + vertexOffset));
			}
		}
		else
		{
			if(Random.Range(0, 56) == 1)
				SpawnObject(vertices[v]);
		}
	}

	void SpawnObject(Vector3 pos)
	{
		//if(Random.Range(0, 140) == 1)
		//{
		//	GameObject s = Instantiate(structure);
		//	s.transform.localPosition = pos;
		//	return;
		//}	

		GameObject obj = ObjectPool.GetObject(TerrainGenerator.instance.GetBiomeRandomObjectData(currentBiome).name);
		if (obj != null)
		{
			obj.transform.SetParent(transform);
			obj.transform.localPosition = pos;
			obj.SetActive(true);

			myObjects.Add(obj);
			InfiniteGenerator.objectsInUse++;

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
			{
				myObjects[i].SetActive(false);
				InfiniteGenerator.objectsInUse--;
			}
		}

		myObjects.Clear();
	}

}
