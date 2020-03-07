using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Pickable : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        InventorySystem.instance.AddItem(name.Replace(" (entity)", ""));
        Destroy(gameObject);
    }
}
