using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recipe", menuName = "New Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public CraftingRecipeItem[] requiredItems;
    public Item itemToCraft;
}

[System.Serializable]
public class CraftingRecipeItem
{
    public Item itemNeeded;
    public int amountNeeded;
}
