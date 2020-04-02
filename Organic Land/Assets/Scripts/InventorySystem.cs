using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    public GameObject itemTeplate;
    public Transform slotsParent;
    public Transform slotSelector;
    public Text selectedItemTooltipText;
    public GameObject objectTemplate;

    public ItemData[] items;

    private Transform selectedSlot;

    private const string tipRightClick = "Right-click to Use (or press F)";

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
        UpdateItemTooltip();
    }

    public void AddItemViaName(string itemName)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].name == itemName)
            {
                AddItem(items[i]);
            }
        }
    }

    public void AddItemViaID(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == itemID)
            {
                AddItem(items[i]);
            }
        }
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
                    selectedItemTooltipText.text = "";

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

        selectedItemTooltipText.text = "";
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

    public Sprite GetItemSprite(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == itemID)
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

    public GameObject GetSelectedItem()
    {
        if(selectedSlot != null && selectedSlot.childCount != 0)
        {
            return selectedSlot.GetChild(0).gameObject;
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
        UpdateItemTooltip();
    }

    public void SelectSlot(int index)
    {
        if (index >= 0 && index < slotsParent.childCount)
        { 
            selectedSlot = slotsParent.GetChild(index);
            UpdateSlotSelectorPosition();

            if (slotsParent.GetChild(index).childCount != 0)
                UpdateItemTooltip(CheckIfItemIsUsable(slotsParent.GetChild(index).GetChild(0).gameObject));
            else
                UpdateItemTooltip();
        }
    }

    public void SelectSlot(Transform targetSlot)
    {
        selectedSlot = targetSlot;
        UpdateSlotSelectorPosition();

        if (targetSlot.childCount != 0)
            UpdateItemTooltip(CheckIfItemIsUsable(targetSlot.GetChild(0).gameObject));
        else
            UpdateItemTooltip();

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

    public void UseSelectedItem()
    {
        if (selectedSlot.childCount == 0)
            return;

        if(selectedSlot.GetChild(0).GetComponent<Item>().isFood)
        {
            RemoveSelectedItem();
            PlayerStats.instance.Add("hunger", 10f);
        }
    }

    public bool CheckIfItemExists(string itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].id == itemID)
                return true;
        }
        return false;
    }

    void AddItem(ItemData data)
    {
        // Instantiate item template
        GameObject newItem = Instantiate(itemTeplate);

        // Initialize & Sort
        InitializeItem(newItem, data);
        SortItem(newItem.transform);

        // Update Text Display
        UpdateItemTooltip(CheckIfItemIsUsable(newItem));

        // Output log
        Debug.Log("> Added item");
    }

    void InitializeItem(GameObject newItem, ItemData itemData)
    {
        newItem.name = itemData.name;
        newItem.GetComponent<Image>().sprite = itemData.icon;

        newItem.GetComponent<Item>().id = itemData.id;
        newItem.GetComponent<Item>().isTool = itemData.isTool;
        newItem.GetComponent<Item>().toolType = itemData.toolType;
        newItem.GetComponent<Item>().isFood = itemData.isFood;
    }

    void SortItem(Transform newItem)
    {
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            if(slotsParent.GetChild(i).childCount == 0)
            {
                newItem.SetParent(slotsParent.GetChild(i));
                newItem.localPosition = new Vector3(0, 0, 0);
                newItem.localScale = new Vector3(1, 1, 1);
                EventManager.ItemAdded();
                return;
            }
        }
        InventoryScreen.instance.StoreNewItemInInventory(newItem);
    }

    bool CheckIfItemIsUsable(GameObject itemObj)
    {
        Item item = itemObj.GetComponent<Item>();
        return item.isFood;
    }

    void UpdateSlotSelectorPosition()
    {
        slotSelector.position = selectedSlot.position;
    }

    void UpdateItemTooltip(bool usable = false)
    {
        if (selectedSlot.childCount != 0)
        {
            if (usable)
                selectedItemTooltipText.text = string.Format("{0}\n{1}", selectedSlot.GetChild(0).name, tipRightClick);
            else
                selectedItemTooltipText.text = selectedSlot.GetChild(0).name;
        }
        else
            selectedItemTooltipText.text = "";
    }
}
