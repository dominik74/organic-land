using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryScreen.instance.ProcessSlot(transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryScreen.instance.SelectSlot(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryScreen.instance.DeselectSlot();
    }
}
