using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object")]
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
    public LootTable lootTable;

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
