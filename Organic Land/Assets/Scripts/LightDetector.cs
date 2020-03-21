using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventManager.OnTimeUpdated += OnTimeUpdated;
    }

    private void OnDisable()
    {
        EventManager.OnTimeUpdated -= OnTimeUpdated;
    }

    void OnTimeUpdated()
    {
        Color newColor = spriteRenderer.color;
        newColor *= 0.5f;
        newColor.a = 1;
        spriteRenderer.color = newColor;
        Debug.Log("Change color");
    }

}
