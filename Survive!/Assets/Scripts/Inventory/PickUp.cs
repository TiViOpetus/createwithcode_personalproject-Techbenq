using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public Item item;

    //REDO LATER
    public bool naturalResource;
    public int prefabNumber;

    //Tries to pick up the object
    public override void Interact()
    {
        base.Interact();
        if (InventoryManager.instance.AddItem(item))
        {
            if (naturalResource)
            {
                ObjectGeneration.instance.availableVerts.Add(transform.position);
                ObjectGeneration.instance.prefabs[prefabNumber].amount--;
            }
            Destroy(gameObject);
        }
    }
}
