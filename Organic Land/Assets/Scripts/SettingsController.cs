using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

	public void SetRenderDistance(Slider slider)
	{
		Settings.renderDistance = Settings.renderDistanceValues[(int)slider.value];
		InfiniteGenerator.instance.Regenerate();
	}

	public void SetMovementSpeed(Slider slider)
	{
		float value = slider.value + 1;
		PlayerManager.instance.player.GetComponent<PlayerMovement>().movementSpeed = value * 10;
	}

	public void SetSmoothRotation(Toggle toggle)
	{
		Settings.smoothRotation = toggle.isOn;
	}
	
	public void SetFullscreen(Toggle toggle)
	{
		Screen.fullScreen = toggle.isOn;
	}

}
