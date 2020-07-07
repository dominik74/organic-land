using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

	public Transform[] categories;

	private bool initalized;

	void Start()
	{
		if(!initalized)
		{
			// Init settings
			SetControl("RenderDistance", 2);
			SetControl("MovementSpeed", 0);
			SetControl("SmoothRotation", Settings.smoothRotation);
			SetControl("GenerateStructures", Settings.generateStructures);
			SetControl("ScreenAnimations", Settings.screenAnimations);
			SetControl("UseSeed", Settings.useSeed);

			Debug.Log("<color=green>Settings Initialized!</color>");
			initalized = true;
		}
	}

	void SetControl(string name, object value)
	{
		for (int i = 0; i < categories.Length; i++)
		{
			for (int y = 0; y < categories[i].childCount; y++)
			{
				if(categories[i].GetChild(y).name.Contains(name))
				{
					if (categories[i].GetChild(y).name.Contains("Toggle"))
					{
						Toggle togg = categories[i].GetChild(y).GetComponent<Toggle>();

						MuteControl(togg.onValueChanged);
						togg.isOn = (bool)value;
						UnmuteControl(togg.onValueChanged);
					}
					else if (categories[i].GetChild(y).name.Contains("Slider"))
					{
						Slider slider = categories[i].GetChild(y).GetChild(1).GetComponent<Slider>();

						MuteControl(slider.onValueChanged);
						slider.value = (int)value;
						UnmuteControl(slider.onValueChanged);
					}
				}
			}
		}
	}

	void MuteControl(UnityEngine.Events.UnityEventBase ev)
	{
		int count = ev.GetPersistentEventCount();

		for (int i = 0; i < count; i++)
		{
			ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
		}
	}

	void UnmuteControl(UnityEngine.Events.UnityEventBase ev)
	{
		int count = ev.GetPersistentEventCount();

		for (int i = 0; i < count; i++)
		{
			ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
		}
	}

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

	public void SetGenerateStructures(Toggle toggle)
	{
		Settings.generateStructures = toggle.isOn;
		InfiniteGenerator.instance.Regenerate();
	}
	
	public void SetFullscreen(Toggle toggle)
	{
		Screen.fullScreen = toggle.isOn;
	}

	public void SetUseSeed(Toggle toggle)
	{
		Settings.useSeed = toggle.isOn;
		InfiniteGenerator.instance.Regenerate();
	}

	public void SetNewSeed(InputField inputField)
	{
		SeedGenerator.SetSeed(inputField.text);
		InfiniteGenerator.instance.Regenerate();
	}

	public void SetScreenAnimations(Toggle toggle)
	{
		Settings.screenAnimations = toggle.isOn;
	}

}
