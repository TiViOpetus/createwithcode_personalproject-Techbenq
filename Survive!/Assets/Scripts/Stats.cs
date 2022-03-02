using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    
    public void TakeDMG(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
