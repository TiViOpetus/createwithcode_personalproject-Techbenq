using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public Transform dropSpot;

    public GameObject itemToDrop;
    public float dropChance;
    public float attackSpeed;

    //Takes damage
    public override void TakeDMG(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            if(itemToDrop != null)
            {
                int rand = Random.Range(0, 100);

                if (rand <= dropChance)
                {
                    Instantiate(itemToDrop, dropSpot.position , itemToDrop.transform.rotation);
                }
            }

            EnemySpawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
