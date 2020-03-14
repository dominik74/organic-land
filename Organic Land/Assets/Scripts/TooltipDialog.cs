using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipDialog : MonoBehaviour {

    public Text itemName;
    public Text itemType;

    public void UpdateTooltip(GameObject targetItem)
    {
        itemName.text = targetItem.name;
        itemType.text = "Object";
    }

}
