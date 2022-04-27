using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Simple class for stats

    public float maxHealth;
    public float currentHealth;

    public ParticleSystem getHit;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public virtual void TakeDMG(float dmg)
    {
        getHit.Play();
    }
}
