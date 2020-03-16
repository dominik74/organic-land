using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.childCount != 0)
            CraftingScreen.instance.DisplayItem(transform.GetChild(0).gameObject);
    }

}
