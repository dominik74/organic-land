using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTween : MonoBehaviour {

	private static CustomTween instance;
	void Awake()
	{
		instance = this;
	}

	public static void MoveY(GameObject target, float to, float time)
	{
		RectTransform targetRect = target.GetComponent<RectTransform>();
		instance.StopCoroutine("Co_MoveY");
		instance.StartCoroutine(Co_MoveY(targetRect, to, time));
	}

	static IEnumerator Co_MoveY(RectTransform rect, float to, float time)
	{
		float currentTime = 0;
		float normalizedValue;

		while (currentTime <= time)
		{
			currentTime += Time.unscaledDeltaTime;
			normalizedValue = currentTime / time;

			rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x, to), normalizedValue);
			yield return null;
		}
	}
}
