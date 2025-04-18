﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour,  IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InventorySystem.instance.SelectSlot(transform);

        if(transform.childCount != 0)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
                InventorySystem.instance.UseSelectedItem();
        }
    }

}
