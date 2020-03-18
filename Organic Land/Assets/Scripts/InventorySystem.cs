﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    public GameObject itemTeplate;
    public Transform slotsParent;
    public Transform slotSelector;
    public Text selectedItemNameText;
    public GameObject objectTemplate;

    public ItemData[] items;

    private Transform selectedSlot;

    #region SINGLETON
    public static InventorySystem instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        selectedSlot = slotsParent.GetChild(0);
        Invoke("UpdateSlotSelectorPosition", 0.2f);      
        UpdateNameText();
    }

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

                // Update Text Display
                UpdateNameText();

                // Output log
                Debug.Log("> Added item");
            }
        }
    }

    public void ConvertAndAddItem(string itemName)
    {
        itemName = itemName.Replace("_", " ");
        itemName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(itemName.ToLower());
        AddItem(itemName);
        Debug.Log(itemName);
    }

    public void RemoveItem(string name)
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            if(slotsParent.GetChild(i).childCount != 0)
            {
                if (slotsParent.GetChild(i).GetChild(0).name == name)
                {
                    Destroy(slotsParent.GetChild(i).GetChild(0).gameObject);
                    selectedItemNameText.text = "";

                    Debug.Log("> Removed item");
                    return;
                }
            }
        }
    }

    public void RemoveSelectedItem()
    {
        if (selectedSlot.childCount != 0)
            Destroy(selectedSlot.GetChild(0).gameObject);

        selectedItemNameText.text = "";
        Debug.Log("> Removed item");
    }

    public GameObject GetItem(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == itemName)
            {
                GameObject newItem = Instantiate(itemTeplate);
                InitializeItem(newItem, items[i]);
                return newItem;
            }
        }
        return null;
    }

    public Sprite GetItemSprite(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == itemName)
                return items[i].icon;
        }
        return null;
    }

    public ItemData GetItemData(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == itemName)
                return items[i];
        }
        return null;
    }

    public void Clear()
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            if (slotsParent.GetChild(i).childCount != 0)
                Destroy(slotsParent.GetChild(i).GetChild(0).gameObject);
        }
        UpdateNameText();
    }

    public void SelectSlot(int index)
    {
        if (index >= 0 && index < slotsParent.childCount)
        { 
            selectedSlot = slotsParent.GetChild(index);
            UpdateSlotSelectorPosition();
            UpdateNameText();
        }
    }

    public void DropSelectedItem()
    {
        RemoveSelectedItem();

        if (selectedSlot.childCount == 0)
            return;
        GameObject droppedItem = Instantiate(objectTemplate);
        droppedItem.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selectedSlot.GetChild(0).GetComponent<Image>().sprite;
        droppedItem.transform.position = PlayerManager.playerUnit.transform.position;
        droppedItem.name = selectedSlot.GetChild(0).name;
        droppedItem.AddComponent<Pickable>();
    }

    public bool CheckIfItemExists(string itemName)
    {
        itemName = itemName.Replace("_", " ");
        itemName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(itemName.ToLower());

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].name == itemName)
                return true;
        }
        return false;
    }

    void InitializeItem(GameObject newItem, ItemData itemData)
    {
        newItem.name = itemData.name;
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

    void UpdateSlotSelectorPosition()
    {
        slotSelector.position = selectedSlot.position;
    }

    void UpdateNameText()
    {
        if (selectedSlot.childCount != 0)
            selectedItemNameText.text = selectedSlot.GetChild(0).name;
        else
            selectedItemNameText.text = "";
    }
}
