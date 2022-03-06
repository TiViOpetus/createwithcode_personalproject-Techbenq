using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    public virtual void TakeDMG(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
