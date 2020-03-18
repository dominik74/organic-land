using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public float terrainSize;

    [Range(1, 300)]
    public int maxObjectCount;

    [Space]
    public GameObject objectTemplate;
    public ObjectData[] objectData;

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        int objectsSpawned = 0;

        while(objectsSpawned < maxObjectCount)
        {
            float random = Random.Range(0, 100);

            int lowLimit = 0;
            int highLimit = 0;

            for (int i = 0; i < objectData.Length; i++)
            {
                lowLimit = highLimit;
                highLimit += objectData[i].spawnChance;

                if (random >= lowLimit && random < highLimit)
                {
                    GameObject obj = Instantiate(objectTemplate);
                    InitializeObject(obj, objectData[i]);
                    obj.transform.position = new Vector3(Random.Range(-terrainSize, terrainSize), 0, Random.Range(-terrainSize, terrainSize));

                    objectsSpawned++;
                    break;
                }

            }
        }

        Debug.Log(objectsSpawned);

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
            obj.GetComponent<Minable>().loot = data.lootDrop;
        }
    }

}
