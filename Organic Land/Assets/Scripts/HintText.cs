using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintText : MonoBehaviour {

    private void Update()
    {
        if(Time.frameCount % 4 == 0)
        {
            if(InventoryScreen.instance != null)
            {
                if (InventoryScreen.instance.inventoryOpened)
                    Destroy(gameObject);
            }

        }
    }

}
