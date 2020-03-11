using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ObjectData")]
public class ObjectData : ScriptableObject {

    public Sprite sprite;
    public int spawnChance = 100;
    public Color color = Color.white;

    [Space]
    public bool pickable;

}
