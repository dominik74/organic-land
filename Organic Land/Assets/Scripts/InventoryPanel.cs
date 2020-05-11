using UnityEngine;

public class InventoryPanel : MonoBehaviour {

	public Transform inventoryPanel;
	public GameObject craftItemWindow;
	public GameObject sideHint;

	private void OnEnable()
	{
		InventoryScreen.instance.OnActivate();
		inventoryPanel.SetAsLastSibling();
		craftItemWindow.SetActive(false);
		sideHint.SetActive(false);
	}

}
