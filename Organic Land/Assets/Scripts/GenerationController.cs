using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationController : MonoBehaviour {

    public float offsetX;
    public float offsetZ;

    [Space]
    public float scale = 1.0f;
    public float smoothness = 100f;
    public float minimumHeight = 1.6f;

    [Space]
    public bool autoRefreshTerrain;
    [Range(0.25f, 3f)]
    public float terrainRefreshRate = 0.5f;

    private bool lastRefreshState;

	public static GenerationController instance;
	void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(autoRefreshTerrain)
        {
            StartCoroutine("AutoTerrainRefresher");
        }
        lastRefreshState = autoRefreshTerrain;
    }

    void Update()
    {
        if(lastRefreshState != autoRefreshTerrain)
        {
            if(autoRefreshTerrain)
            {
                StopCoroutine("AutoTerrainRefresher");
                StartCoroutine("AutoTerrainRefresher");
            }
            else
            {
                StopCoroutine("AutoTerrainRefresher");
            }
            lastRefreshState = autoRefreshTerrain;
        }
    }

    IEnumerator AutoTerrainRefresher()
    {
        while (true)
        {
            yield return new WaitForSeconds(terrainRefreshRate);
            InfiniteGenerator.instance.Regenerate();
        }
    }

}
