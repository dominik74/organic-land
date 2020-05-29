using UnityEngine;

public class InventoryPanel : MonoBehaviour {

	public GameObject craftItemWindow;
	public GameObject sideHint;

	private void OnEnable()
	{
		InventoryScreen.instance.OnActivate();
		craftItemWindow.SetActive(false);
		sideHint.SetActive(false);
	}

}
