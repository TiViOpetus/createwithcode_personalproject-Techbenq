using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetCraftingSlot : MonoBehaviour
{
    public CraftingRecipe recipe;
    public Image craftingLogo;

    public GameObject requiredItems;
    public GameObject requiredItem;


    //Edits the crafting slot according to the items needed
    private void Start()
    {
        craftingLogo.sprite = recipe.itemToCraft.itemLogo;
        craftingLogo.enabled = true;

        foreach (CraftingRecipeItem it in recipe.requiredItems)
        {
            GameObject temp;
            temp = Instantiate(requiredItem, requiredItems.transform);

            temp.GetComponentInChildren<Image>().sprite = it.itemNeeded.itemLogo;
            temp.GetComponentInChildren<Image>().enabled = true;
            temp.GetComponentInChildren<Text>().text = it.amountNeeded + "X";
        }

        GetComponent<CraftingButton>().itemToCraft = recipe;
        Destroy(this);
    }
}
