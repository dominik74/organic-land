using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minable : MonoBehaviour, IObjectController
{
    public Tools collectTool;
    public LootTable lootTable;

    public const float maxDurability = 100f;
    public float currentDurability;

    private Transform player;
    private float minMineDistance = 2.5f;

    private Durability currentTool;

	// Initialize
	void Start() 
	{
		player = PlayerManager.playerUnit.transform;
	}
	
	// Reset
    void OnEnable()
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
            {
                currentTool = selectedItem.GetComponent<Durability>();
                if ((player.position - transform.position).sqrMagnitude <= minMineDistance * minMineDistance)
                {
                    TakeDmg();
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
        currentTool.TakeDamage();
        TakeDamage(5f);
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
        LootSystem.instance.DropLootTable(lootTable, transform.position);
        gameObject.SetActive(false);
    }
}
