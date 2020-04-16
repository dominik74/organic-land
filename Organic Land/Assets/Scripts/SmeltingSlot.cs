using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmeltingSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.childCount != 0)
            SmeltingScreen.instance.DisplayItem(transform.GetChild(1).gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SmeltingScreen.instance.SelectSlot(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SmeltingScreen.instance.DeselectSlot();
    }
}
