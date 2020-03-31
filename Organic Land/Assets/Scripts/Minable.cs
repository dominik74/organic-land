using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour, IObjectController
{
    public Tools collectTool;
    public string loot;
    public int lootCount;

    public const float maxDurability = 100f;
    public float currentDurability;

    private Transform player;
    private float minMineDistance = 2.5f;

    void Start()
    {
        currentDurability = maxDurability;
        player = PlayerManager.playerUnit.transform;
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
            {
                if ((player.position - transform.position).sqrMagnitude <= minMineDistance * minMineDistance)
                {
                    TakeDamage(15f);
                }
                else
                {
                    PlayerMovement.MoveToTarget(transform.position, minMineDistance, TakeDmg);
                }
            }
        }
    }

    void TakeDmg()
    {
        TakeDamage(15f);
    }

    void TakeDamage(float dmg)
    {
        if (transform.GetChild(0).GetComponent<ObjectAnimator>())
            transform.GetChild(0).GetComponent<ObjectAnimator>().Animate();

        currentDurability -= dmg;
        if (currentDurability <= 0)
        {
            currentDurability = 0;
            Die();
        }
    }

    void Die()
    {
        LootSystem.instance.DropItem(loot, transform.position, lootCount);
        Destroy(gameObject);
    }
}
