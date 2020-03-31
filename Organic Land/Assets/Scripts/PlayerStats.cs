using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float maxHealth = 100;
    public float maxHunger = 150;
    public float maxThirst = 125;

    [Space]
    public float hungerDropRate = 0.25f;
    public float healthDropRate = 0.75f;

    private float currentHealth;
    private float currentHunger;
    private float currentThirst;

    private bool starving;

    public class PlayerStatsContainer
    {
        public float health;
        public float hunger;
        public float thirst;

        public PlayerStatsContainer(float health, float hunger, float thirst)
        {
            this.health = health;
            this.hunger = hunger;
            this.thirst = thirst;
        }
    }

    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetStats();

        StartCoroutine("HungerTimer");
    }

    public PlayerStatsContainer GetCurrentStats()
    {
        return new PlayerStatsContainer(currentHealth, currentHunger, currentThirst);
    }

    public void Add(string statName, float amount)
    {
        switch (statName)
        {
            case "health":
                currentHealth += amount;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;
                break;
            case "hunger":
                currentHunger += amount;
                if (currentHunger > maxHunger)
                    currentHunger = maxHunger;
                starving = false;
                break;
            case "thirst":
                currentThirst += amount;
                if (currentThirst > maxThirst)
                    currentThirst = maxThirst;
                break;
        }

    }

    void ResetStats()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }

    void Die()
    {
        Debug.Log("You died!");
    }

    IEnumerator HungerTimer()
    {
        while (true)
        {
            if(currentHunger > 0)
            {
                currentHunger--;

                EventManager.StatsUpdated();
            }
            else
            {
                if(!starving)
                {
                    currentHunger = 0;
                    StopCoroutine("DamageTimer");
                    StartCoroutine("DamageTimer");
                    starving = true;
                }
            }
            yield return new WaitForSeconds(hungerDropRate);
        }
    }

    IEnumerator DamageTimer()
    {
        while (currentHunger <= 0)
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
                yield break;
            }
            EventManager.StatsUpdated();
            yield return new WaitForSeconds(healthDropRate);
        }
    }

}
