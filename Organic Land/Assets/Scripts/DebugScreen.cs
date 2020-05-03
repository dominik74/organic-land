using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour {
	
	public float refreshRate = 0.5f;
	
	private Text debugTxt;
	private float timer;
	
	void Start()
	{
		debugTxt = GetComponent<Text>();
	}
	
	void Update()
	{
		if (Time.unscaledTime > timer)
		{
			Refresh();
			timer = Time.unscaledTime + refreshRate;
		}
	}
	
	void Refresh()
	{
		Vector3 playerPos = PlayerManager.playerUnit.transform.position;
		
		debugTxt.text = string.Format("X: {0} / Z: {1}", Mathf.RoundToInt(playerPos.x), Mathf.RoundToInt(playerPos.z));
	}
	
}
