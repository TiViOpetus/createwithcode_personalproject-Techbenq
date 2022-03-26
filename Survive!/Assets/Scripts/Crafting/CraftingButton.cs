using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    public CraftingRecipe itemToCraft;
    private List<CraftingRecipeItem> removedItems = new List<CraftingRecipeItem>();

    private void Start()
    {
        GetComponentInChildren<Button>().onClick.AddListener(Craft);
    }

    //Tries to remove the items for the recipe
    //If lacks the item it adds it back
    public void Craft()
    {
        foreach(CraftingRecipeItem it in itemToCraft.requiredItems)
        {
            if (!InventoryManager.instance.RemoveItems(it.itemNeeded, it.amountNeeded))
            {
                ReturnItems();
                return;
            }
            removedItems.Add(it);
        }

        if (!InventoryManager.instance.AddItem(itemToCraft.itemToCraft))
        {
            ReturnItems();
        }
    }

    //Returns the items if crafting fails
    public void ReturnItems()
    {
        foreach (CraftingRecipeItem it in removedItems)
        {
            for(int i = 0; i < it.amountNeeded; i++)
            {
                InventoryManager.instance.AddItem(it.itemNeeded);
            }
        }
    }
}
