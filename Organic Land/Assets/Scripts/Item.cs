using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [HideInInspector] public string id;
    [HideInInspector] public bool isTool;
    [HideInInspector] public bool isFood;
    [HideInInspector] public bool isBuildable;
    [HideInInspector] public Tools toolType;

}
