using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipe
{
    public int[] requiredItems;
    public int itemToCraft;

    public CraftRecipe(int[] _requiredItems, int _itemToCraft)
    {
        this.requiredItems = _requiredItems;
        this.itemToCraft = _itemToCraft;
    }

    public CraftRecipe(CraftRecipe recipe)
    {
        this.requiredItems = recipe.requiredItems;
        this.itemToCraft = recipe.itemToCraft;
    }

}
