using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour {

    public int maxDurability;
    private int currentDurability;
    private Text durabilityDisplayText;

    private void Start()
    {
        currentDurability = maxDurability;
        transform.GetChild(0).gameObject.SetActive(true);
        durabilityDisplayText = transform.GetChild(0).GetComponent<Text>();
        UpdateUI();
    }

    public void SetMaxDurability(int amount)
    {
        maxDurability = amount;
    }

    public void TakeDamage()
    {
        currentDurability--;

        if (currentDurability <= 0)
            Destroy(gameObject);

        UpdateUI();
    }

    void UpdateUI()
    {
        float percentage = ((float)currentDurability / maxDurability) * 100f;
        durabilityDisplayText.text = string.Format("{0}%", Mathf.Round(percentage));
    }

}
