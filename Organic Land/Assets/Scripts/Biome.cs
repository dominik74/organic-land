using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "Biome")]
public class Biome : ScriptableObject {

	public Color primaryColor;
	public Color secondaryColor;

	public float height;
	public float offset;

	public ObjectData[] objects;

}
