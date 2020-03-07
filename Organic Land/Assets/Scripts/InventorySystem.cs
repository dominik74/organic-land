using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    public GameObject itemTeplate;
    public Transform slotsParent;

    public ItemData[] items;

    #region SINGLETON
    public static InventorySystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void AddItem(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].name == name)
            {
                // Instantiate item template
                GameObject newItem = Instantiate(itemTeplate);

                // Initialize & Sort
                InitializeItem(newItem, items[i]);
                SortItem(newItem.transform);

                // Output log
                Debug.Log("Added item");
            }
        }
    }

    void InitializeItem(GameObject newItem, ItemData itemData)
    {
        newItem.GetComponent<Image>().sprite = itemData.icon;
    }

    void SortItem(Transform newItem)
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            if(slotsParent.GetChild(i).childCount == 0)
            {
                newItem.SetParent(slotsParent.GetChild(i));
                newItem.localPosition = new Vector3(0, 0, 0);
                return;
            }
        }
    }
}
