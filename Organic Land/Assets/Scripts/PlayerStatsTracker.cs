using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsTracker : MonoBehaviour {

    public Slider healthbar, hungerbar, thirstbar;

    private PlayerStats playerStats;

    private void Start()
    {
        InitializeStatusBars();
    }

    private void OnEnable()
    {
        EventManager.OnStatsUpdated += UpdateStats;
    }

    private void OnDisable()
    {
        EventManager.OnStatsUpdated -= UpdateStats;
    }

    void InitializeStatusBars()
    {
        healthbar.maxValue = playerStats.maxHealth;
        hungerbar.maxValue = playerStats.maxHunger;
        thirstbar.maxValue = playerStats.maxThirst;

        healthbar.value = healthbar.maxValue;
        hungerbar.value = hungerbar.maxValue;
        thirstbar.value = thirstbar.maxValue;
    }

    void UpdateStats()
    {
        if (playerStats == null) // Prevent Null Reference Errors (quick workaround)
            playerStats = PlayerStats.instance;

        healthbar.value = playerStats.GetCurrentStats().health;
        hungerbar.value = playerStats.GetCurrentStats().hunger;
        thirstbar.value = playerStats.GetCurrentStats().thirst;
    }

}
