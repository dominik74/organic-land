using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {

	public static readonly int[] renderDistanceValues = { 3, 5, 7, 10 };
	public static int renderDistance = renderDistanceValues[2];

	public static bool smoothRotation = true;
	public static bool generateStructures = false;

}
