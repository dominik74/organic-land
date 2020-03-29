using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItemData")]
public class ItemData : ScriptableObject {

    public Sprite icon;

    [Space]
    public bool isTool;
    public Tools toolType;

    [Space]
    public bool isFood;

    [Space]
    public bool craftable;
    public Materials[] materials;

}

[System.Serializable]
public class Materials
{
    public string name;
    public int count;
}
