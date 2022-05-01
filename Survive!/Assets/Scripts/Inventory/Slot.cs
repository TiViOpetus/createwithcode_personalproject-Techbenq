using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Image slotImage;
    public Item slotItem;

    public Text slotText;

    public int itemAmount;


    private void Start()
    {
        slotText = GetComponentInChildren<Text>();
    }

    //Adds an item to the slot returns true if slot had space
    public bool AddItem(Item item)
    {
        if(itemAmount > 0)
        {
            if (itemAmount < item.maxStack)
            {
                itemAmount++;
                slotText.text = itemAmount.ToString();
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
            slotText.text = itemAmount.ToString();
            return true;
        }
    }
    
    //Removes given amount of items from the slot
    //Returns 0 if given amount is removed
    public int RemoveItem(int amount, bool activeSlot = false)
    {
        if(itemAmount <= amount)
        {
            int i = itemAmount;
            itemAmount = 0;
            slotText.text = "";

            if (activeSlot) slotItem.Unactivate();

            slotItem = null;
            slotImage.enabled = false;

            return amount - i;
        }
        itemAmount -= amount;

        slotText.text = itemAmount.ToString();

        return 0;
    }

    //Tries to use the item in the slot
    public void Use()
    {
        if(slotItem != null)
        {
            if (slotItem.Use())
            {
                RemoveItem(1);
                if (itemAmount <= 0) ToolSlot.instance.RemoveTool();
            }
        }
    }
}
