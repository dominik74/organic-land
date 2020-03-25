using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviour {

    public Transform dirLight;
    public float timeBtwStage = 10f;
    public float dayLength = 60f;
    public bool secondHalf;

    [Space]
    public Slider timebar;

    private float timeSpeed = 1;
    private float currentTime;

    private float currentStage;

    public static DayNightController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentStage = timeBtwStage;
        timebar.maxValue = dayLength;
        timebar.value = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;

        if(currentTime >= dayLength)
        {
            ResetDay();
            return;
        }
        else if (currentTime >= currentStage)
        {
            dirLight.Rotate(new Vector3(timeBtwStage/dayLength * 360, 0, 0));
            currentStage += timeBtwStage;

            if (currentTime >= dayLength / 2)
                secondHalf = true;
            else
                secondHalf = false;

            TerrainGenerator.UpdateLighting();
            UpdateUI();
        }

        // Debug
        if (Time.frameCount % 60 == 0)
            Debug.Log(currentTime);
    }

    void ResetDay()
    {
        currentTime = 0;
        currentStage = timeBtwStage;
        timebar.value = 0;
    }

    void UpdateUI()
    {
        timebar.value = currentTime;
    }

}
