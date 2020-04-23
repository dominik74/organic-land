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

}
