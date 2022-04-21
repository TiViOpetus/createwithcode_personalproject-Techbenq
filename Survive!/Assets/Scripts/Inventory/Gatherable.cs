using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public int prefabIndex;
    public Item[] itemsQuaranteed;
    public GatherItem[] itemsWithChance;
    public int health;

    public void Gather()
    {
        foreach(Item item in itemsQuaranteed)
        {
            InventoryManager.instance.AddItem(item);
        }

        foreach (GatherItem item in itemsWithChance)
        {
            float rand = Random.Range(0, 100);
            if (rand <= item.dropChance)
            {
                InventoryManager.instance.AddItem(item.item);
            }
        }

        health -= 1;
        if(health <= 0)
        {
            ObjectGeneration.instance.availableVerts.Add(transform.position);
            ObjectGeneration.instance.prefabs[prefabIndex].amount--;
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class GatherItem
{
    public Item item;
    public float dropChance;
}
