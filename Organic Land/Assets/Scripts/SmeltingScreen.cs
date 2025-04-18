﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmeltingScreen : MonoBehaviour {

    public Transform smeltingSlotsParent;
    public GameObject itemWindow;
    public Slider progressbar;

    [Space]
    public Transform slotSelector;

    private InventorySystem inventorySystem;
    private SmeltingSystem smeltingSystem;

    private string selectedItemID;

    public static SmeltingScreen instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventorySystem = InventorySystem.instance;
        smeltingSystem = SmeltingSystem.instance;

        progressbar.value = 0;
        slotSelector.gameObject.SetActive(false);

        InitializeSmeltingSlots();
    }

    public void SelectSlot(Transform targetSlot)
    {
        slotSelector.gameObject.SetActive(true);
        slotSelector.transform.position = targetSlot.position;
    }

    public void DeselectSlot()
    {
        slotSelector.gameObject.SetActive(false);
    }

    public void DisplayItem(GameObject targetItem)
    {
        itemWindow.SetActive(true);
        itemWindow.GetComponent<ItemWindow>().UpdateWindow(targetItem);
        selectedItemID = targetItem.GetComponent<Item>().id;
    }

    public void SmeltSelectedItem()
    {
        if (selectedItemID != null)
            smeltingSystem.SmeltItem(selectedItemID);
    }

    public void SetProgressbar(bool state)
    {
        progressbar.gameObject.SetActive(state);
        if (state == true)
            ResetProgressbar();
    }

    public void UpdateProgressbar(float newValue)
    {
        progressbar.value = newValue;
    }

    void ResetProgressbar()
    {
        progressbar.value = 0;
    }

    void InitializeSmeltingSlots()
    {
        for (int i = 0; i < smeltingSystem.smeltableItems.Count; i++)
        {
            GameObject newItem = inventorySystem.GetItem(smeltingSystem.smeltableItems[i].name);
            newItem.transform.SetParent(smeltingSlotsParent.GetChild(i));
            newItem.transform.localScale = new Vector3(1.25f, 1.25f, 0);
            newItem.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

}
