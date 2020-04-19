using UnityEngine;

public class InventoryPanel : MonoBehaviour {

	private void OnEnable()
	{
		InventoryScreen.instance.OnActivate();
	}

}
