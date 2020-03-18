using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour, IObjectController
{
    public CollectTool collectTool;
    public string loot;

    private const float maxDurability = 100f;
    private float currentDurability;

    void Start()
    {
        currentDurability = maxDurability;
    }

    public void Interact()
    {
        TakeDamage(15f);
    }

    void TakeDamage(float dmg)
    {
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
