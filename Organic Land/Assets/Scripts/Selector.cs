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

        HUDController.instance.DisplayObjectLabel(transform.parent.name);
        ProcessDurabilityStatus();
        HUDController.instance.DrawObjectLabel();
    }

    public void Deselect()
    {
        spriteRenderer.color = defaultColor;
        HUDController.instance.DisplayObjectLabel("");
        HUDController.instance.DrawObjectLabel();
    }

    public void Press()
    {
        spriteRenderer.color = pressedColor;

        ProcessDurabilityStatus();
        HUDController.instance.DrawObjectLabel();
    }

    public void UnPress()
    {
        spriteRenderer.color = darkerColor;
    }

    void ProcessDurabilityStatus()
    {
        Minable minable = transform.parent.GetComponent<Minable>();

        if (minable != null)
            HUDController.instance.UpdateDurabilityStatus(minable.currentDurability, Minable.maxDurability);
        else
            HUDController.instance.UpdateDurabilityStatus(0f, 0f);
    }

}
