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

    private bool starving;

    private PlayerStatsContainer stats;

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
        return stats;
    }

    public void Add(string statName, float amount)
    {
        switch (statName)
        {
            case "health":
                stats.health += amount;
                if (stats.health > maxHealth)
                    stats.health = maxHealth;
                break;
            case "hunger":
                stats.hunger += amount;
                if (stats.hunger > maxHunger)
                    stats.hunger = maxHunger;
                starving = false;
                break;
            case "thirst":
                stats.thirst += amount;
                if (stats.thirst > maxThirst)
                    stats.thirst = maxThirst;
                break;
        }
        EventManager.StatsUpdated();
    }

    public void TakeDamage(float amount)
    {
        stats.health -= amount;
        if (stats.health <= 0)
        {
            stats.health = 0;
            Die();
        }
        EventManager.StatsUpdated();
    }

    void ResetStats()
    {
        stats = new PlayerStatsContainer(maxHealth, maxHunger, maxThirst);
    }

    void Die()
    {
        Debug.Log("You died!");
    }

    IEnumerator HungerTimer()
    {
        while (true)
        {
            if(stats.hunger > 0)
            {
                stats.hunger--;
                EventManager.StatsUpdated();
            }
            else
            {
                if(!starving)
                {
                    stats.hunger = 0;
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
        while (stats.hunger <= 0)
        {
            stats.health--;
            if (stats.health <= 0)
            {
                stats.health = 0;
                Die();
                yield break;
            }
            EventManager.StatsUpdated();
            yield return new WaitForSeconds(healthDropRate);
        }
    }

}

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
