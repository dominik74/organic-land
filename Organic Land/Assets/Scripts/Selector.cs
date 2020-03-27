using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private Color defaultColor;
    private Color darkerColor;
    private Color pressedColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        defaultColor = spriteRenderer.color;
        darkerColor = spriteRenderer.color * 0.5f;
        darkerColor.a = 1;
        pressedColor = spriteRenderer.color * 0.25f;
        pressedColor.a = 1;
    }

    public void Select()
    {
        Debug.Log("Selected");
        spriteRenderer.color = darkerColor;

        HUDController.instance.DisplayObjectName(transform.parent.name);
    }

    public void Deselect()
    {
        spriteRenderer.color = defaultColor;
        HUDController.instance.DisplayObjectName("");
    }

    public void Press()
    {
        spriteRenderer.color = pressedColor;
    }

    public void UnPress()
    {
        spriteRenderer.color = darkerColor;
    }

}
