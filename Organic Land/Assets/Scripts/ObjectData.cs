using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ObjectData")]
public class ObjectData : ScriptableObject {

    public Sprite sprite;
    public string id;
    public int spawnChance = 100;
    public Color color = Color.white;

    [Space]
    public CollectBehavior collectBehavior;
    [Tooltip("Change this ONLY if collectBehavior is minable.")]
    public Tools collectTool;

    [Space]
    [Tooltip("Change this ONLY if collectBehavior is minable.")]
    public string lootDrop;
    public int lootCount = 1;

}

public enum CollectBehavior
{
    none,
    pickable,
    minable
}

public enum Tools
{
    none,
    axe,
    pickaxe,
}
