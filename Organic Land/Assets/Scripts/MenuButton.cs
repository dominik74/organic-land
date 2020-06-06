using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    public static MenuButton current;

    public bool isTab;
    public bool selectedTab;

    private Color highlightColor;
    private Color defaultColor;
    private Color pressedColor;

    private Text myText;
    private Image image;

    private ButtonState btnState;
    private ButtonState State 
    { 
        get { return btnState; }
        set
        {
            btnState = value;

            switch (btnState)
            {
                case ButtonState.none:
                    myText.color = defaultColor;
                    if (image != null)
                        image.color = defaultColor;
                    break;
                case ButtonState.highlighted:
                    myText.color = highlightColor;
                    if (image != null)
                        image.color = highlightColor;
                    break;
                case ButtonState.pressed:
                    myText.color = pressedColor;
                    if (image != null)
                        image.color = pressedColor;
                    break;
                default:
                    break;
            }
        }
    }

    private bool initialized;

    private void Start()
    {
        myText = transform.GetChild(0).GetComponent<Text>();
        defaultColor = myText.color;
        highlightColor = GetComponent<Button>().colors.highlightedColor;
        pressedColor = GetComponent<Button>().colors.pressedColor;

        if (transform.childCount == 2)
            image = transform.GetChild(1).GetComponent<Image>();

        if (isTab && selectedTab)
        {
            State = ButtonState.highlighted;
            current = this;
        }

        initialized = true;
    }

    void OnEnable()
    {
        if (initialized)
        {
            if (!isTab)
                State = ButtonState.none;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isTab)
            State = ButtonState.highlighted;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isTab)
            State = ButtonState.none;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        State = ButtonState.pressed;

        if (isTab && current != this)
        {
            current.Deactivate();
            current = this;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        State = ButtonState.highlighted;
    }

    public void Deactivate()
    {
        State = ButtonState.none;
    }

    private enum ButtonState
    {
        none, highlighted, pressed
    }
}
