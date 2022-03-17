using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCraftingSlots : MonoBehaviour
{
    public GameObject craftingSlot;
    public CraftingRecipe[] recipes;

    private void Start()
    {
        int i = 0;
        foreach(CraftingRecipe recipe in recipes)
        {

            GameObject temp;
            temp = Instantiate(craftingSlot, transform);
            temp.GetComponent<SetCraftingSlot>().recipe = recipes[i];
            i++;
        }
        Destroy(this);
    }
}
