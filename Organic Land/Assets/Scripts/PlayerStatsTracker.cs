using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsTracker : MonoBehaviour {

    public Slider healthbar, hungerbar, thirstbar;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerStats.instance;
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
    }

    void UpdateStats()
    {
        healthbar.value = playerStats.GetCurrentStats().health;
        hungerbar.value = playerStats.GetCurrentStats().hunger;
        thirstbar.value = playerStats.GetCurrentStats().thirst;
    }

}
