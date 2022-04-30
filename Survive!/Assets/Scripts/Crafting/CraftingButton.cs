using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    public CraftingRecipe itemToCraft;
    private Button craftingButton;
    private void Start()
    {
        craftingButton = GetComponentInChildren<Button>();
        craftingButton.onClick.AddListener(Craft);
    }

    //Tries to remove the items for the recipe
    //If lacks the item it adds it back
    public void Craft()
    {
        if(SurvivalNeeds.instance.godMode)
        {
            InventoryManager.instance.AddItem(itemToCraft.itemToCraft);
            craftingButton.interactable = false;
            craftingButton.interactable = true;
            return;
        }

        int i = 0;
        foreach(CraftingRecipeItem it in itemToCraft.requiredItems)
        {
            if (!InventoryManager.instance.RemoveItems(it.itemNeeded, it.amountNeeded))
            {
                AddBack(i);
                return;
            }
            i++;
        }
        InventoryManager.instance.AddItem(itemToCraft.itemToCraft);

        craftingButton.interactable = false;
        craftingButton.interactable = true;
    }

    //adds back the removed items
    private void AddBack(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            for(int ii = 0; ii < itemToCraft.requiredItems[i].amountNeeded; ii++)
            {
                InventoryManager.instance.AddItem(itemToCraft.requiredItems[i].itemNeeded);
            }
        }
    }
}
