using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Slot[] slots;

    public CanvasGroup craftingMenu;

    public Slot activeSlot;

    private Button[] craftingButtons;

    public static InventoryManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        craftingButtons = craftingMenu.GetComponentsInChildren<Button>();
        SetActiveSlot(0);
    }


    //Tries to add item to one of the slots if all are full return false
    public bool AddItem(Item item)
    {
        foreach(Slot sl in slots)
        {
            if(sl.slotItem == item || sl.slotItem == null)
            {
                if (sl.AddItem(item))
                {
                    if(sl == activeSlot)
                    {
                        sl.slotItem.Activate();
                    }
                    return true;
                }
            }
        }
        return false;
    }

    //Removes certain item a certain amount
    //Adds them back if it couldnt remove the given amount
    public bool RemoveItems(Item item, int amount)
    {
        int leftOvers = amount;
        foreach(Slot sl in slots)
        {
            if (sl.slotItem == item)
            {
                if(sl == activeSlot) leftOvers = sl.RemoveItem(leftOvers, true);
                else leftOvers = sl.RemoveItem(leftOvers);

                if(leftOvers <= 0)
                {
                    return true;
                }
            }
        }
        for(int i = 0; i < amount - leftOvers; i++)
        {
            AddItem(item);
        }
        return false;
    }

    //Sets currently active slot
    public void SetActiveSlot(int slotNum)
    {

        if (activeSlot != null)
        {
            activeSlot.GetComponent<Image>().color -= new Color(0.2f, 0.2f, 0.2f);

            if (activeSlot.slotItem != null)
                activeSlot.slotItem.Unactivate();
        }

        activeSlot = slots[slotNum];
        activeSlot.GetComponent<Image>().color += new Color(0.2f, 0.2f, 0.2f);

        if(activeSlot.slotItem != null)
            activeSlot.slotItem.Activate();
    }

    //Toggles on crafting menu
    public void ToggleCrafting(bool toggle)
    {
        if(toggle)
        {
            craftingMenu.alpha = 1;
        }
        else
        {
            craftingMenu.alpha = 0;
        }
        foreach (Button bt in craftingButtons)
        {
            bt.enabled = toggle;
        }

    }
}

