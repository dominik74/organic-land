using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour, IObjectController
{
    public Tools collectTool;
    public string loot;

    private const float maxDurability = 100f;
    private float currentDurability;

    void Start()
    {
        currentDurability = maxDurability;
    }

    public void Interact()
    {
        DetectCorrectToolType();
    }

    void DetectCorrectToolType()
    {
        GameObject selectedItem = InventorySystem.instance.GetSelectedItem();
        if(selectedItem != null)
        {
            if (selectedItem.GetComponent<Item>().toolType == collectTool)
                TakeDamage(15f);
        }
    }

    void TakeDamage(float dmg)
    {
        if (transform.GetChild(0).GetComponent<ObjectAnimator>())
            transform.GetChild(0).GetComponent<ObjectAnimator>().Animate();

        currentDurability -= dmg;
        if (currentDurability <= 0)
            Die();
    }

    void Die()
    {
        InventorySystem.instance.AddItem(loot);
        Destroy(gameObject);
    }
}
