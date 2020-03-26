using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    private Color highlightColor;
    private Color defaultColor;
    private Color pressedColor;

    private Text myText;

    private void Start()
    {
        myText = transform.GetChild(0).GetComponent<Text>();
        defaultColor = myText.color;
        highlightColor = GetComponent<Button>().colors.highlightedColor;
        pressedColor = GetComponent<Button>().colors.pressedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.color = defaultColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        myText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        myText.color = defaultColor;
    }
}
