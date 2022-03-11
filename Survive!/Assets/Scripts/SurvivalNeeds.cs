using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalNeeds : Stats
{
    public static bool withinRadius = true;

    public float maxHunger;
    public float maxWarmth;

    private float currentHunger;
    private float currentWarmth;

    public Slider staminaSlid, healthSlid, hungerSlid, coldSlid;

    private void Start()
    {
        currentHunger = maxHunger;
        currentWarmth = maxWarmth;
        InvokeRepeating("GetCold", 4, 0.1f);
        InvokeRepeating("GetHungry", 4, 0.15f);
    }
    public override void TakeDMG(float dmg)
    {
        base.TakeDMG(dmg);
        healthSlid.value = currentHealth / maxHealth;
    }

    private void GetCold()
    {
        if (withinRadius)
        {
            if (currentWarmth < 100)
            {
                currentWarmth += maxWarmth * 0.005f;
                coldSlid.value = currentWarmth / maxWarmth;
            }
        }
        else
        {
            if (currentWarmth > 0)
            {
                currentWarmth -= maxWarmth * 0.0025f;
                coldSlid.value = currentWarmth / maxWarmth;
            }
        }

        if (currentWarmth <= 10)
        {
            TakeDMG(maxHealth * 0.005f);
        }
    }
    private void GetHungry()
    {
        currentHunger = Mathf.Clamp(currentHunger - maxHunger * 0.0025f, 0, maxHunger);
        hungerSlid.value = currentHunger / maxHunger;

        if (currentHunger <= 0)
        {
            TakeDMG(maxHealth * 0.01f);
        }
    }
}
