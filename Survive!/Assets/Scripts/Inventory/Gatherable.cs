using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public ToolType toolTypeNeeded;
    public int toolLevelRequired = 0;

    public int prefabIndex;
    public Item[] itemsQuaranteed;
    public GatherItem[] itemsWithChance;
    public int health;

    public AudioClip[] audioClips;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void Gather()
    {
        int i = Random.Range(0, audioClips.Length);
        source.PlayOneShot(audioClips[i]);

        if (ToolSlot.instance.currentToolType == toolTypeNeeded)
        {
            if (ToolSlot.instance.currentToolLevel == toolLevelRequired)
            {
                foreach (Item item in itemsQuaranteed)
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
                if (health <= 0)
                {
                    ObjectGeneration.instance.availableVerts.Add(transform.position);
                    ObjectGeneration.instance.prefabs[prefabIndex].amount--;
                    Invoke("Remove", audioClips[i].length);
                }
            }

            else if (ToolSlot.instance.currentToolLevel > toolLevelRequired)
            {
                foreach (Item item in itemsQuaranteed)
                {
                    InventoryManager.instance.AddItem(item);
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

                health -= 2;
                if (health <= 0)
                {
                    ObjectGeneration.instance.availableVerts.Add(transform.position);
                    ObjectGeneration.instance.prefabs[prefabIndex].amount--;
                    Invoke("Remove", audioClips[i].length);
                }
            }
        }
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}

[System.Serializable]
public class GatherItem
{
    public Item item;
    public float dropChance;
}
