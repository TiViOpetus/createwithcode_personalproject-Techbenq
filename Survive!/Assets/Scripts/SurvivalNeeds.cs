using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurvivalNeeds : Stats
{
    public bool rolling = false;
    public bool torchActive;

    public bool godMode;

    public static bool withinRadius = true;

    public float maxStamina;
    public float maxHunger;
    public float maxWarmth;

    public float hungerDmg;
    public float coldDmg;

    public float coldRate;
    public float hungerRate;
    public float regenRate;
    public float staminaCd;
    public float regenCd;

    private float currentStamina;
    private float currentHunger;
    private float currentWarmth;

    private bool canRegen = true;

    private bool alive = true;

    public Slider staminaSlid, healthSlid, hungerSlid, coldSlid;

    public static SurvivalNeeds instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        currentStamina = maxStamina;
        currentHunger = maxHunger;
        currentWarmth = maxWarmth;

        if (!godMode)
        {
            StartCoroutine(GetCold());
            StartCoroutine(GetHungry());
        }
    }

    //Take Damage if godmode then dont
    public override void TakeDMG(float dmg)
    {
        if (godMode || rolling)
            return;

        canRegen = false;
        StopCoroutine(Regen());
        StopCoroutine(AllowRegen());
        StartCoroutine(AllowRegen());

        base.TakeDMG(dmg);
        currentHealth -= dmg;
        healthSlid.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator Regen()
    {
        while (canRegen)
        {
            yield return new WaitForSeconds(regenRate);
            currentHealth += maxHealth * 0.02f;
            healthSlid.value = currentHealth / maxHealth;

            if (currentHealth >= maxHealth) canRegen = false;
        }
    }

    private IEnumerator AllowRegen()
    {
        yield return new WaitForSeconds(regenCd);
        canRegen = true;
        StartCoroutine(Regen());
    }


    //Makes the player get cold more cold at night
    //if player is within campfire collider it will warm up instead
    private IEnumerator GetCold()
    {

        while (alive)
        {
            yield return new WaitForSeconds(coldRate);

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
                    if (torchActive)
                    {
                        //get less cold if you have torch lit
                        if (!DayNightCycle.isDay) currentWarmth -= maxWarmth * 0.005f  / 2;
                        else currentWarmth -= maxWarmth * 0.002f / 2;
                    }
                    else
                    {
                        if (!DayNightCycle.isDay) currentWarmth -= maxWarmth * 0.005f;
                        else currentWarmth -= maxWarmth * 0.002f;
                    }

                    coldSlid.value = currentWarmth / maxWarmth;
                }
            }

            if (currentWarmth <= 10)
            {
                TakeDMG(maxHealth * (coldDmg / 100));
            }
        }
    }

    //Food calls this and adds stats
    public void Eat(float value)
    {
        currentHunger = Mathf.Clamp(currentHunger + value, 0, maxHunger);
        hungerSlid.value = currentHunger / maxHunger;
    }

    //Makes the player get hungry
    private IEnumerator GetHungry()
    {
        while (alive)
        {
            yield return new WaitForSeconds(hungerRate);
            currentHunger = Mathf.Clamp(currentHunger - maxHunger * 0.0025f, 0, maxHunger);
            hungerSlid.value = currentHunger / maxHunger;

            if (currentHunger <= 0)
            {
                TakeDMG(maxHealth * (hungerDmg / 100));
            }
        }
    }

    //Drains stamina and returns true if there was enough stamina
    public bool DrainStamina(float amount)
    {
        if (godMode)
            return true;

        if (currentStamina - amount < 0)
            return false;

        CancelInvoke();
        currentStamina -= amount;
        staminaSlid.value = currentStamina / maxStamina;
        
        InvokeRepeating("GainStamina", staminaCd, 0.1f);

        return true;
    }

    //Gain stamina at steady rate
    public void GainStamina()
    {
        if(currentStamina < 100)
        {
            currentStamina += maxStamina * 0.025f;
            currentStamina = Mathf.Clamp(currentStamina, 0, 100);
            staminaSlid.value = currentStamina / maxStamina;
            GetHungry();
        }
    }
}
