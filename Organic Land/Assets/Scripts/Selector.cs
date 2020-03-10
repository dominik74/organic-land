using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public BoxCollider boxCollider;

    private SpriteRenderer spriteRenderer;

    private Color darkerColor;

    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Vector2 size = spriteRenderer.sprite.bounds.size;
        boxCollider.size = size;
        boxCollider.center = new Vector3(0, size.x, 0);

        darkerColor = spriteRenderer.color * 0.5f;
        darkerColor.a = 1;
    }

    public void Select()
    {
        Debug.Log("Selected");
        spriteRenderer.color = darkerColor;
    }

}
