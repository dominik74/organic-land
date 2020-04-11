using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public float terrainSize;

    private static List<GameObject> spawnedObjects = new List<GameObject>();

    [Range(1, 300)]
    public int maxObjectCount;

    [Space]
    public GameObject objectTemplate;
    public ObjectData[] objectData;

    public static TerrainGenerator instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Generate();
    }
  
    public ObjectData GetObjectDataViaName(string objName)
    {
        for (int i = 0; i < objectData.Length; i++)
        {
            if (objectData[i].name == objName)
                return objectData[i];
        }
        return null;
    }

    public ObjectData GetObjectDataViaID(string id)
    {
        for (int i = 0; i < objectData.Length; i++)
        {
            if (objectData[i].id == id)
                return objectData[i];
        }
        return null;
    }

    void Generate()
    {
        int objectsSpawned = 0;

        for (int i = 0; i < objectData.Length; i++)
        {
            for (int y = 0; y < objectData[i].spawnChance; y++)
            {
                SpawnObject(objectData[i]);
                objectsSpawned++;
            }
        }

        Debug.Log(objectsSpawned);
    }

    void SpawnObject(ObjectData data)
    {
        GameObject obj = Instantiate(objectTemplate);
        InitializeObject(obj, data);
        obj.transform.position = new Vector3(Random.Range(-terrainSize, terrainSize), 0, Random.Range(-terrainSize, terrainSize));

        spawnedObjects.Add(obj);
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
