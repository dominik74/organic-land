using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour {

	public string stringSeed = "seed";
	public bool useStringSeed;

	public bool useRandomSeed;
	public static int worldSeed;

	public static int seedX;
	public static int seedZ;

	void Awake()
	{
		if (useStringSeed)
		{
			worldSeed = stringSeed.GetHashCode();
		}

		if (useRandomSeed)
		{
			Random.InitState(System.DateTime.Now.Millisecond);
			worldSeed = Random.Range(-999999, 999999);
		}

		InitSeedXZ();
	}

	public static void SetSeed(string newSeedString)
	{
		int newSeedInt = -1;

		if (int.TryParse(newSeedString, out newSeedInt))
			worldSeed = newSeedInt;
		else
			worldSeed = newSeedString.GetHashCode();

		InitSeedXZ();
	}

	static void InitSeedXZ()
	{
		string seedString = worldSeed.ToString();

		if (seedString.Length > 1)
		{
			int splitX = Mathf.FloorToInt(seedString.Length / 2);

			seedX = int.Parse(seedString.Substring(0, splitX));
			seedZ = int.Parse(seedString.Substring(splitX));
		}
		else
		{
			seedX = worldSeed;
			seedZ = 0;
		}
		Debug.Log(string.Format("seedX: {0}, seedZ: {1}", seedX, seedZ));
	}

}
