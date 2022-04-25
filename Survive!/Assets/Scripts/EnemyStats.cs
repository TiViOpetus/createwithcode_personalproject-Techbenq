using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public LayerMask groundMask;
    public GameObject itemToDrop;
    public float dropChance;
    public float attackSpeed;

    //Takes damage and rolls for item drop when dies
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
                    Vector3 spawnPos;
                    Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 10, groundMask);
                    spawnPos = hit.point;
                    Instantiate(itemToDrop, spawnPos , itemToDrop.transform.rotation);
                }
            }

            EnemySpawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
