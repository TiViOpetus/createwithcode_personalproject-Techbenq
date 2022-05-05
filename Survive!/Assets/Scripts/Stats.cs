using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Simple class for stats

    public float maxHealth;
    public float currentHealth;

    public ParticleSystem getHit;

    public AudioClip dmgSound;
    public AudioSource source;
    public virtual void TakeDMG(float dmg)
    {
        getHit.Play();
        source.PlayOneShot(dmgSound);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
