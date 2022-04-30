using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static List<Vector3> spawnLocations;
    public static int enemyCount;

    public static int maxEnemies = 2;
    public int spawnDelay;

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    private void Start()
    {
        InvokeRepeating("SummonEnemy", spawnDelay * 2, spawnDelay);
    }

    //Summons an enemy if its night time in random available location
    void SummonEnemy()
    {
        if (!DayNightCycle.isDay)
        {
            if (enemyCount < maxEnemies)
            {
                int randomPos = Random.Range(0, spawnLocations.Count - 1);

                Instantiate(enemyPrefab, spawnLocations[randomPos], enemyPrefab.transform.rotation);
                enemyCount += 1;
            }
        }

        if (DayNightCycle.bossSpawn)
        {
            DayNightCycle.bossSpawn = false;
            Instantiate(bossPrefab, bossPrefab.transform.position, enemyPrefab.transform.rotation);
        }
    }
}
