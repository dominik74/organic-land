using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour {

	public string stringSeed = "seed";
	public bool useStringSeed;

	public bool useRandomSeed;
	public static int seed;

	void Awake()
	{
		if (useStringSeed)
		{
			seed = stringSeed.GetHashCode();
		}

		if (useRandomSeed)
		{
			seed = Random.Range(0, 999999);
		}

		Random.InitState(seed);
	}

}
