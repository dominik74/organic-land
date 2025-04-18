﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour {

    public int maxDurability;
    public int currentDurability;
    private Text durabilityDisplayText;

    private bool initialized;
    private bool hideDurability;

    private void Start()
    {
        Initialize();
        UpdateUI();
    }

    public void SetMaxDurability(int amount)
    {
        maxDurability = amount;
        currentDurability = maxDurability;
        Initialize();
        UpdateUI();
    }

    public void TakeDamage()
    {
        currentDurability--;

        if (currentDurability <= 0)
            Destroy(gameObject);

        UpdateUI();
    }

    public void SetDurability(int newValue)
    {
        currentDurability = newValue;
        UpdateUI();
    }

    public void HideDurability()
    {
        hideDurability = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Initialize()
    {
        if(!initialized)
        {
            if(!hideDurability)
                transform.GetChild(0).gameObject.SetActive(true);
            durabilityDisplayText = transform.GetChild(0).GetComponent<Text>();
            initialized = true;
        }
    }

    void UpdateUI()
    {
        float percentage = ((float)currentDurability / maxDurability) * 100f;
        durabilityDisplayText.text = string.Format("{0}%", Mathf.Round(percentage));
    }

}
