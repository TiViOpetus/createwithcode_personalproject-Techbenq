using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public Item itemToDrop;
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
                InventoryManager.instance.AddItem(itemToDrop);
            }

            EnemySpawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
