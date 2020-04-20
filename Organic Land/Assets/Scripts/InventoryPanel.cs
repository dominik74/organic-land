using UnityEngine;

public class InventoryPanel : MonoBehaviour {

	public Transform inventoryPanel;
	public GameObject craftItemWindow;

	private void OnEnable()
	{
		InventoryScreen.instance.OnActivate();
		inventoryPanel.SetAsLastSibling();
		craftItemWindow.SetActive(false);
	}

}
