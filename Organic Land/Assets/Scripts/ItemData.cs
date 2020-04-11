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

    [Space]
    public bool isFood;

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
