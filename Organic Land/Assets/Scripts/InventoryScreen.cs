using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {

    public Transform hotbarParent;
    public Transform invHotbarParent;

    private Transform[] hotbarSlots;
    private Transform[] invHotbarSlots;

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
        initialized = true;
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
    }

    public void ProcessSlot(Transform targetSlot)
    {
        if(selectedItem != null)
        {
            if (targetSlot.childCount == 0)
                PlaceItem(targetSlot);
            else
            {
                PlaceItem(targetSlot);
                GrabItem(targetSlot);
            }
        }
        else
        {
            GrabItem(targetSlot);
        }
        SyncHotbar();
    }

    void GrabItem(Transform targetSlot)
    {
        if(targetSlot.childCount != 0)
        {
            selectedItem = targetSlot.GetChild(0);

            selectedItem.SetParent(transform);
            selectedItem.transform.localScale = new Vector3(1.7f, 1.7f, 1);
        }
    }

    void PlaceItem(Transform targetSlot)
    {
        selectedItem.SetParent(targetSlot);
        selectedItem.transform.localScale = new Vector3(1.4f, 1.4f, 1);
        selectedItem.transform.localPosition = new Vector3(0, 0, 0);
        selectedItem = null;
    }

    public void SelectSlot(Transform target)
    {
        selectedSlot = target;
    }

    public void DeselectSlot()
    {
        selectedSlot = null;
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

    void CopyItemTo(GameObject itemToCopy, Transform parent)
    {
        GameObject newItem = Instantiate(itemToCopy);

        newItem.transform.SetParent(parent);
        newItem.transform.localPosition = new Vector3(0, 0, 0);
        newItem.name = itemToCopy.name;

        if (parent.parent.name == invHotbarParent.name)
            ScaleItem(newItem.transform, 1.4f);
        else
            ScaleItem(newItem.transform, 1f);
    }

    void ScaleItem(Transform itemToScale, float amount)
    {
        itemToScale.localScale = new Vector3(amount, amount, 1);
    }

}
