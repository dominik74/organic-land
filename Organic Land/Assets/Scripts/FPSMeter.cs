using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSMeter : MonoBehaviour {

    public float refreshRate = 1f;
    public static int currentFps;

    private Text myText;
    private float timer;

    private void Start()
    {
        myText = GetComponent<Text>();
    }

    private void Update()
    {
        if(Time.unscaledTime > timer)
        {
            currentFps = (int)(1f / Time.unscaledDeltaTime);

            myText.text = string.Format("{0} FPS", currentFps);

            timer = Time.unscaledTime + refreshRate;
        }

    }

}
