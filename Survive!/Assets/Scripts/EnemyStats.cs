using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public GameObject itemToDrop;
    public float dropChance;
    public float attackSpeed;
    public override void TakeDMG(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            int rand = Random.Range(0, 100);

            if(rand <= dropChance)
            {
                Instantiate(itemToDrop, transform.position + Vector3.down * 0.85f, itemToDrop.transform.rotation);
            }

            EnemySpawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
