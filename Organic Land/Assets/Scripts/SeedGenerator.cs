using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGenerator : MonoBehaviour {

	public string stringSeed = "seed";
	public bool useStringSeed;

	public bool useRandomSeed;
	public static int worldSeed;

	void Awake()
	{
		if (useStringSeed)
		{
			worldSeed = stringSeed.GetHashCode();
		}

		if (useRandomSeed)
		{
			worldSeed = Random.Range(0, 999999);
		}
	}

	public static void SetSeed(UnityEngine.UI.InputField inputField)
	{
		string newSeed = inputField.text;
		worldSeed = newSeed.GetHashCode();
	}

}
