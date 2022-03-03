using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        if(InventoryManager.instance.AddItem(item))
            Destroy(gameObject);
    }
}
