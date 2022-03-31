using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour
{
    public CraftingRecipe itemToCraft;

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
                return;
            }
        }
        InventoryManager.instance.AddItem(itemToCraft.itemToCraft);
    }
}
