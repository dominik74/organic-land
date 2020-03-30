using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWindow : MonoBehaviour {

    public Text itemName;
    public Text itemType;
    public Transform requiredItemsParent;
    public GameObject requiredItemTemplate;

    public void UpdateWindow(GameObject targetItem)
    {
        itemName.text = targetItem.name;
        itemType.text = "Object";

        UpdateRequiredItems(InventorySystem.instance.GetItemData(targetItem.name));
    }

    void UpdateRequiredItems(ItemData itemData)
    {
        ClearRequiredItems();
        for (int i = 0; i < itemData.materials.Length; i++)
        {
            GameObject rit = Instantiate(requiredItemTemplate);

            // Initialize
            rit.GetComponent<Image>().sprite = InventorySystem.instance.GetItemSprite(itemData.materials[i].id);
            rit.transform.GetChild(0).GetComponent<Text>().text = itemData.materials[i].count.ToString();

            rit.transform.SetParent(requiredItemsParent);
            rit.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void ClearRequiredItems()
    {
        for (int i = 0; i < requiredItemsParent.childCount; i++)
        {
            Destroy(requiredItemsParent.GetChild(i).gameObject);
        }
    }

}
