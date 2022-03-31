using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Image slotImage;
    public Item slotItem;

    private int itemAmount;

    public bool AddItem(Item item)
    {
        if(itemAmount > 0)
        {
            if (itemAmount < item.maxStack)
            {
                itemAmount++;
                return true;
            }
            else return false;
        }
        else
        {
            slotImage.sprite = item.itemLogo;
            slotImage.enabled = true;
            slotItem = item;
            itemAmount++;
            return true;
        }
    }
    public int RemoveItem(Item item, int amount)
    {
        if(itemAmount <= amount)
        {
            int i = itemAmount;
            itemAmount = 0;
            slotItem = null;
            slotImage.enabled = false;

            return amount - i;
        }
        itemAmount -= amount;

        return 0;
    }

    public void Use()
    {
        if(slotItem != null)
        {
            if (slotItem.Use())
            {
                RemoveItem(slotItem, 1);
            }
        }
    }
}
