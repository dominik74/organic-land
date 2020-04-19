using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {

    public Transform hotbarParent;
    public Transform invHotbarParent;
    public Transform invSlotsParent;

    public Transform slotSelector;
    [Space]
    public GameObject tooltipDialog;
    public Vector2 tooltipOffset;

    [HideInInspector] public bool inventoryOpened;

    private Transform[] hotbarSlots;
    private Transform[] invHotbarSlots;
    private Transform[] invSlots;

    private bool initialized;

    private Transform selectedItem;
    private Transform selectedSlot;

    public static InventoryScreen instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CacheSlots();
        SyncInventoryHotbar();
        slotSelector.gameObject.SetActive(false);
        tooltipDialog.SetActive(false);
        initialized = true;
        inventoryOpened = true;
    }

    private void OnEnable()
    {
        if (initialized)
            SyncInventoryHotbar();
    }

    private void Update()
    {
        if (selectedItem != null)
            selectedItem.position = Input.mousePosition;

        if (tooltipDialog.activeSelf)
            tooltipDialog.transform.position = Input.mousePosition + (Vector3)tooltipOffset;
    }

    public void ProcessSlot(Transform targetSlot)
    {
        if (targetSlot.childCount == 0)
        {
            if(selectedItem != null)
                PlaceItem(targetSlot);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                QuickSort(targetSlot.GetChild(0));
            }
            else
            {
                if (selectedItem != null)
                {
                    PlaceItem(targetSlot);
                    GrabItem(targetSlot);

                    tooltipDialog.SetActive(true);
                    tooltipDialog.GetComponent<TooltipDialog>().UpdateTooltip(selectedItem.gameObject);
                }
                else
                {
                    GrabItem(targetSlot);
                }
            }
        }
        SyncHotbar();
    }

    void GrabItem(Transform targetSlot)
    {
        if(targetSlot.childCount != 0)
        {
            selectedItem = targetSlot.GetChild(0);

            selectedItem.SetParent(transform);
            selectedItem.transform.localScale = new Vector3(1.55f, 1.55f, 1);

            InventorySystem.instance.UpdateItemTooltip();
        }
    }

    void PlaceItem(Transform targetSlot)
    {
        selectedItem.SetParent(targetSlot);
        selectedItem.transform.localScale = new Vector3(1.25f, 1.25f, 1);
        selectedItem.transform.localPosition = new Vector3(0, 0, 0);
        selectedItem = null;

        InventorySystem.instance.UpdateItemTooltip();
    }

    public void SelectSlot(Transform target)
    {
        selectedSlot = target;
        slotSelector.gameObject.SetActive(true);
        slotSelector.transform.position = selectedSlot.position;
        if(target.childCount != 0 && selectedItem == null)
        {
            tooltipDialog.SetActive(true);
            tooltipDialog.GetComponent<TooltipDialog>().UpdateTooltip(target.GetChild(0).gameObject);
        }
    }

    public void DeselectSlot()
    {
        selectedSlot = null;
        slotSelector.gameObject.SetActive(false);
        if(selectedItem == null)
            tooltipDialog.SetActive(false);
    }

    public GameObject[] FindItems(string itemID, int limit = 9999)
    {
        List<GameObject> items = new List<GameObject>();
        for (int i = 0; i < invSlots.Length; i++)
        {
            if(invSlots[i].childCount != 0)
            {
                if (invSlots[i].GetChild(0).GetComponent<Item>().id == itemID)
                {
                    if (items.Count < limit)
                        items.Add(invSlots[i].GetChild(0).gameObject);
                    else
                        return items.ToArray();
                }
            }
        }

        for (int i = 0; i < invHotbarSlots.Length; i++)
        {
            if (invHotbarSlots[i].childCount != 0)
            {
                if (invHotbarSlots[i].GetChild(0).GetComponent<Item>().id == itemID)
                {
                    if (items.Count < limit)
                        items.Add(invHotbarSlots[i].GetChild(0).gameObject);
                    else
                        return items.ToArray();
                }
            }
        }
        return items.ToArray();
    }

    public void StoreNewItemInInventory(Transform newItem)
    {
        SortInventory(newItem);
        ScaleItem(newItem, 1.25f);
    }

    public void UpdateHotbar()
    {
        UpdateAfterDelay.ExecuteAfterFrame(SyncHotbar);
    }

    void CacheSlots()
    {
        hotbarSlots = new Transform[hotbarParent.childCount];
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i] = hotbarParent.GetChild(i);
        }

        invHotbarSlots = new Transform[invHotbarParent.childCount];
        for (int i = 0; i < invHotbarSlots.Length; i++)
        {
            invHotbarSlots[i] = invHotbarParent.GetChild(i);
        }

        invSlots = new Transform[invSlotsParent.childCount];
        for (int i = 0; i < invSlots.Length; i++)
        {
            invSlots[i] = invSlotsParent.GetChild(i);
        }
    }

    void SyncInventoryHotbar()
    {
        for (int i = 0; i < invHotbarSlots.Length; i++)
        {
            if(invHotbarSlots[i].childCount != hotbarSlots[i].childCount)
            {
                if(hotbarSlots[i].childCount != 0)
                {
                    CopyItemTo(hotbarSlots[i].GetChild(0).gameObject, invHotbarSlots[i]);
                }
                else
                {
                    Destroy(invHotbarSlots[i].GetChild(0).gameObject);
                }
            }
            else
            {
                if(invHotbarSlots[i].childCount != 0)
                {
                    if(invHotbarSlots[i].GetChild(0).name != hotbarSlots[i].GetChild(0).name)
                    {
                        Destroy(invHotbarSlots[i].GetChild(0).gameObject);
                        CopyItemTo(hotbarSlots[i].GetChild(0).gameObject, invHotbarSlots[i]);
                    }
                    else
                    {
                        Durability durability1 = hotbarSlots[i].GetChild(0).GetComponent<Durability>();
                        Durability durability2 = invHotbarSlots[i].GetChild(0).GetComponent<Durability>();
                        if (durability1 != null && durability2 != null)
                        {
                            if (durability2.currentDurability != durability1.currentDurability)
                                durability2.SetDurability(durability1.currentDurability);
                        }
                    }
                }
            }
        }
    }

    void SyncHotbar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if(hotbarSlots[i].childCount != invHotbarSlots[i].childCount)
            {
                if(invHotbarSlots[i].childCount != 0)
                {
                    CopyItemTo(invHotbarSlots[i].GetChild(0).gameObject, hotbarSlots[i]);
                }
                else
                {
                    Destroy(hotbarSlots[i].GetChild(0).gameObject);
                }
            }
            else
            {
                if(hotbarSlots[i].childCount != 0)
                {
                    if(hotbarSlots[i].GetChild(0).name != invHotbarSlots[i].GetChild(0).name)
                    {
                        Destroy(hotbarSlots[i].GetChild(0).gameObject);
                        CopyItemTo(invHotbarSlots[i].GetChild(0).gameObject, hotbarSlots[i]);
                    }
                }
            }
        }
    }

    void QuickSort(Transform targetItem)
    {
        if (targetItem.parent.parent.name == invHotbarParent.name)
            SortInventory(targetItem);
        else
            SortHotbar(targetItem);
    }

    void SortInventory(Transform itemToSort)
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if(invSlots[i].childCount == 0)
            {
                itemToSort.SetParent(invSlots[i]);
                itemToSort.localPosition = new Vector3(0, 0, 0);
                EventManager.ItemAdded();
                return;
            }
        }
    }

    void SortHotbar(Transform itemToSort)
    {
        for (int i = 0; i < invHotbarSlots.Length; i++)
        {
            if (invHotbarSlots[i].childCount == 0)
            {
                itemToSort.SetParent(invHotbarSlots[i]);
                itemToSort.localPosition = new Vector3(0, 0, 0);
                return;
            }
        }
    }

    void CopyItemTo(GameObject itemToCopy, Transform parent)
    {
        GameObject newItem = Instantiate(itemToCopy);

        newItem.transform.SetParent(parent);
        newItem.transform.localPosition = new Vector3(0, 0, 0);
        newItem.name = itemToCopy.name;

        if (parent.parent.name == invHotbarParent.name)
            ScaleItem(newItem.transform, 1.25f);
        else
            ScaleItem(newItem.transform, 1f);
    }

    void ScaleItem(Transform itemToScale, float amount)
    {
        itemToScale.localScale = new Vector3(amount, amount, 1);
    }

}
