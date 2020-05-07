using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour {

	public static string currentBiomeName;
	public static int objAllocateCount;
	public static int chunkAllocateCount;

	public float refreshRate = 0.5f;
	
	private Text debugTxt;
	private float timer;
	
	void Start()
	{
		debugTxt = GetComponent<Text>();
	}
	
	void Update()
	{
		if (Time.unscaledTime > timer)
		{
			Refresh();
			timer = Time.unscaledTime + refreshRate;
		}
	}
	
	void Refresh()
	{
		Vector3 playerPos = PlayerManager.playerUnit.transform.position;
		
		debugTxt.text = string.Format("X: {0} / Z: {1}\nCurrent biome: {2}\nObjects allocated: {3}\nChunks allocated: {4}\nObjects in use: {5}\nChunks in use: {6}", 
			Mathf.RoundToInt(playerPos.x), Mathf.RoundToInt(playerPos.z), currentBiomeName, objAllocateCount, chunkAllocateCount, InfiniteGenerator.objectsInUse, InfiniteGenerator.tilesInUse);
	}
	
}
