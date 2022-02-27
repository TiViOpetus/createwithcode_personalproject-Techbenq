using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static List<Vector3> spawnLocations;
    public int maxEnemies;
    public int spawnDelay;

    public int enemyCount;

    public GameObject enemyPrefab;

    private void Start()
    {
        InvokeRepeating("SummonEnemy", spawnDelay * 2, spawnDelay);
    }

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
    }
}
