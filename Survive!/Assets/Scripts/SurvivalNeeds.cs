using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalNeeds : Stats
{
    public bool godMode;

    public static bool withinRadius = true;

    public float maxStamina;
    public float maxHunger;
    public float maxWarmth;

    public float coldRate;
    public float hungerRate;
    public float staminaCd;

    private float currentStamina;
    private float currentHunger;
    private float currentWarmth;

    public Slider staminaSlid, healthSlid, hungerSlid, coldSlid;

    public static SurvivalNeeds instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentStamina = maxStamina;
        currentHunger = maxHunger;
        currentWarmth = maxWarmth;

        if (!godMode)
        {
            InvokeRepeating("GetCold", 5, coldRate);
            InvokeRepeating("GetHungry", 5, hungerRate);
        }
    }

    //Take Damage
    public override void TakeDMG(float dmg)
    {
        if (godMode)
            return;

        base.TakeDMG(dmg);
        healthSlid.value = currentHealth / maxHealth;
    }


    //Makes the player get cold
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

    //Makes the player get hungry
    private void GetHungry()
    {
        currentHunger = Mathf.Clamp(currentHunger - maxHunger * 0.0025f, 0, maxHunger);
        hungerSlid.value = currentHunger / maxHunger;

        if (currentHunger <= 0)
        {
            TakeDMG(maxHealth * 0.01f);
        }
    }

    //Drains stamina and returns true if there was enough stamina
    public bool DrainStamina(float amount)
    {
        if (godMode)
            return true;

        if (currentStamina - amount < 0)
            return false;

        currentStamina -= amount;
        staminaSlid.value = currentStamina / maxStamina;

        InvokeRepeating("GainStamina", staminaCd, 0.1f);

        return true;
    }

    //Gain stamina at steadily
    public void GainStamina()
    {
        if(currentStamina < 100)
        {
            currentStamina += maxStamina * 0.05f;
            currentStamina = Mathf.Clamp(currentStamina, 0, 100);
            GetHungry();
        }
    }
}
