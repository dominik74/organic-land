using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateLighting()
    {
        Color newColor = spriteRenderer.color;
        if (!DayNightController.instance.secondHalf)
            newColor *= 0.5f;
        else
            newColor /= 0.5f;
        newColor.a = 1;
        spriteRenderer.color = newColor;
        Debug.Log("Change color");
    }

}
