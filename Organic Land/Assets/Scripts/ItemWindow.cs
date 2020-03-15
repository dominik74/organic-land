using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindow : MonoBehaviour {

    public Text itemName;
    public Text itemType;

    public void UpdateWindow(GameObject targetItem)
    {
        itemName.text = targetItem.name;
        itemType.text = "Object";
    }

}
