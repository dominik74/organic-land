using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour,  IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventorySystem.instance.HighlightSlot(transform, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventorySystem.instance.HighlightSlot(null, false);
    }
}
