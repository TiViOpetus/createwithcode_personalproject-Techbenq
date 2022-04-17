using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneration : MonoBehaviour
{
    public Prefab[] prefabs;

    public List<Vector3> availableVerts;

    public Transform player;

    public Transform parent;

    public static ObjectGeneration instance;
    private void Awake()
    {
        instance = this;
    }


    //Creates first set of objects and saves how many of each object there is
    public void CreateFirstObjects()
    {
        List<Vector3> usedVerts = new List<Vector3>();

        for(int i = 0; i < prefabs.Length; i++)
        {
            foreach (Vector3 vector in availableVerts)
            {
                float random = Random.Range(0, 100);

                if(random % 2 == 0)
                {
                    if(random <= prefabs[i].spawnChance)
                    {
                        GameObject temp = Instantiate(prefabs[i].prefab, vector, prefabs[i].prefab.transform.rotation);

                        random = Random.Range(0, 360);
                        temp.transform.Rotate(Vector3.up, random);

                        random = Random.Range(temp.transform.localScale.x / 1.5f, temp.transform.localScale.x * 1.5f);
                        temp.transform.localScale = new Vector3(random, random, random);

                        temp.transform.parent = parent;
                        usedVerts.Add(vector);

                        prefabs[i].amount++;
                    }
                }
            }

            foreach (Vector3 vector in usedVerts) availableVerts.Remove(vector);

            usedVerts.Clear();
        }

        foreach (Prefab prefab in prefabs) prefab.maxAmount = prefab.amount;
        EnemySpawner.spawnLocations = availableVerts;
    }

    //Creates more objects every day
    public void CreateObjects()
    {
        List<Vector3> usedVerts = new List<Vector3>();

        availableVerts.Remove(player.transform.position.normalized);

        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].amount >= prefabs[i].maxAmount) continue;

            for(int current = prefabs[i].amount; current < prefabs[i].maxAmount; current++)
            {
                int randomPos = Random.Range(0, availableVerts.Count);
                GameObject temp = Instantiate(prefabs[i].prefab, availableVerts[randomPos], prefabs[i].prefab.transform.rotation);

                float random = Random.Range(0, 360);
                temp.transform.Rotate(Vector3.up, random);

                random = Random.Range(temp.transform.localScale.x / 1.5f, temp.transform.localScale.x * 1.5f);
                temp.transform.localScale = new Vector3(random, random, random);

                temp.transform.parent = parent;
                usedVerts.Add(availableVerts[randomPos]);

                prefabs[i].amount++;
            }

            foreach (Vector3 vector in usedVerts) availableVerts.Remove(vector);

            usedVerts.Clear();
        }

        EnemySpawner.spawnLocations = availableVerts;
    }
}


[System.Serializable]
public class Prefab
{
    public GameObject prefab;
    public float spawnChance;

    public int amount;
    public int maxAmount;
}