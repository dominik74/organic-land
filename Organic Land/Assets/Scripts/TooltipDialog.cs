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

        if (targetItem.GetComponent<Item>().isTool)
            itemType.text = "Tools";
        else if (targetItem.GetComponent<Item>().isFood)
            itemType.text = "Food";
        else
            itemType.text = "Objects";
    }

}
