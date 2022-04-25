using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceScript : CampfireScript
{
    public override void Interact()
    {
        if (onFire)
        {
            if(InventoryManager.instance.activeSlot.slotItem is Ore ore)
            {
                InventoryManager.instance.RemoveItems(ore, 1);
                InventoryManager.instance.AddItem(ore.barToGet);
            }
        }

        if (burningSticks >= maxSticks) return;

        if (InventoryManager.instance.activeSlot.slotItem == stickItem)
        {
            if (AddStick())
                InventoryManager.instance.activeSlot.RemoveItem(1);
        }
    }
}
