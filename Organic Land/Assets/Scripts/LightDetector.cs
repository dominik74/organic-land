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

    void OnTimeUpdated()
    {
        spriteRenderer.color = Color
    }

}
