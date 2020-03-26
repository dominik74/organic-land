using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float maxHealth = 100;
    public float maxHunger = 150;
    public float maxThirst = 125;

    [Space]
    public float hungerDropRate = 0.25f;

    private float currentHealth;
    private float currentHunger;
    private float currentThirst;

    private void Start()
    {
        ResetStats();

        StartCoroutine("HungerTimer");
    }

    void ResetStats()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }

    IEnumerator HungerTimer()
    {
        while (currentHunger > 0)
        {
            currentHunger--;
            yield return new WaitForSeconds(hungerDropRate);
        }
        currentHunger = 0;
    }

}
