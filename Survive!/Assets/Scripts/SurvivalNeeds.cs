using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalNeeds : Stats
{
    public static bool withinRadius = true;

    public Slider staminaSlid, healthSlid, hungerSlid, coldSlid;
    public override void TakeDMG(float dmg)
    {
        base.TakeDMG(dmg);
        healthSlid.value = currentHealth / maxHealth;
    }

    private void Update()
    {
        Debug.Log(withinRadius);
    }
}
