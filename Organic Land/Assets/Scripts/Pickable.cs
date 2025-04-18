﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    private Transform player;
    private float minPickupDistance = 2.5f;

    private void Start()
    {
        player = PlayerManager.playerUnit.transform;
    }

    public void Pickup()
    {
        if((player.position - transform.position).sqrMagnitude <= minPickupDistance * minPickupDistance)
        {
            InternalPickup();
        }
        else
        {
            PlayerMovement.MoveToTarget(transform.position, minPickupDistance, InternalPickup);
        }
    }

    void InternalPickup()
    {
        EventManager.OnItemAdded = OnItemAdded;
        InventorySystem.instance.AddItemViaName(name.Replace(" (entity)", ""));
    }

    void OnItemAdded()
    {
        EventManager.OnItemAdded = null;
        Destroy(gameObject);
        HUDController.instance.DisplayObjectLabel("");
    }
}
