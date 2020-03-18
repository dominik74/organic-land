using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "ObjectData")]
public class ObjectData : ScriptableObject {

    public Sprite sprite;
    public int spawnChance = 100;
    public Color color = Color.white;

    [Space]
    public CollectBehavior collectBehavior;
    [Tooltip("Change this ONLY if collectBehavior is minable.")]
    public CollectTool collectTool;

    [Space]
    [Tooltip("Change this ONLY if collectBehavior is minable.")]
    public string lootDrop;

}

public enum CollectBehavior
{
    none,
    pickable,
    minable
}

public enum CollectTool
{
    none,
    axe,
    pickaxe,
}
