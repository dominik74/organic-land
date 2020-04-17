using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject {

    public Sprite icon;
    public string id;

    [Space]
    public bool isTool;
    public Tools toolType;
    public int durability = 150;

    [Space]
    public bool isFood;
    public bool smeltable;

    [Space]
    public bool isBuildable;

    [Space]
    public bool craftable;
    public Materials[] materials;

}

[System.Serializable]
public class Materials
{
    public string id;
    public int count;
}
