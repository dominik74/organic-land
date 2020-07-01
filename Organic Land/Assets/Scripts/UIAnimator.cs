using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour {

    public static void StartScreenIntroAnimation(GameObject targetScreen)
    {
        targetScreen.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
        CustomTween.MoveY(targetScreen.transform.GetChild(0).gameObject, 0, 1f);
    }

}
