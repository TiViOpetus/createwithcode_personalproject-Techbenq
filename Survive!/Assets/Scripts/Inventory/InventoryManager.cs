using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Slot[] slots;

    public Slot activeSlot;

    public static InventoryManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        SetActiveSlot(0);
    }

    public bool AddItem(Item item)
    {
        foreach(Slot sl in slots)
        {
            if(sl.slotItem == item || sl.slotItem == null)
            {
                if (sl.AddItem(item))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool RemoveItems(Item item, int amount)
    {
        int leftOvers = amount;
        foreach(Slot sl in slots)
        {
            if (sl.slotItem == item)
            {
                leftOvers = sl.RemoveItem(item, leftOvers);
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

    public void SetActiveSlot(int slotNum)
    {
        if(activeSlot != null)
            activeSlot.GetComponent<Image>().color -= new Color(0.2f,0.2f,0.2f);

        activeSlot = slots[slotNum];
        activeSlot.GetComponent<Image>().color += new Color(0.2f, 0.2f, 0.2f);
    }
}

