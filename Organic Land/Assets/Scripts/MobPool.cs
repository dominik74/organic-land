using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour {

	public GameObject enemyPrefab;
	private static GameObject enemy;

	void Start()
	{
		enemy = enemyPrefab;
	}

	public static void SpawnEnemy(Vector3 pos)
	{
		GameObject obj = Instantiate(enemy);
		obj.transform.position = pos;
	}

}
