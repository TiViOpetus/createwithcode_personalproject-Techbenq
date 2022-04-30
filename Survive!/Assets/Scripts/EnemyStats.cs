using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : Stats
{
    public LayerMask groundMask;
    public GameObject itemToDrop;
    public float dropChance;
    public float attackSpeed;

    public Slider healthBar;
    public Text healthText;

    private void Start()
    {
        if(DayNightCycle.dayNum > 1)
        {
            currentHealth = maxHealth * DayNightCycle.dayNum / 0.75f;
            return;
        }
        currentHealth = maxHealth;
    }

    //Takes damage and rolls for item drop when dies
    public override void TakeDMG(float dmg)
    {
        base.TakeDMG(dmg);
        currentHealth -= dmg;
        healthBar.value = currentHealth / maxHealth;
        healthText.text = (int)currentHealth / maxHealth * 100 + "%";
        if (currentHealth <= 0)
        {
            if(itemToDrop != null)
            {
                int rand = Random.Range(0, 100);

                if (rand <= dropChance)
                {
                    Vector3 spawnPos;
                    Physics.Raycast(transform.position + Vector3.up * 2.5f, Vector3.down, out RaycastHit hit, 10, groundMask);
                    spawnPos = hit.point;
                    Instantiate(itemToDrop, spawnPos , itemToDrop.transform.rotation);
                }
            }

            EnemySpawner.enemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
