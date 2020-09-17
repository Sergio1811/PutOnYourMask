using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecipeDatabase : MonoBehaviour
{
    public List<CraftRecipe> recipes = new List<CraftRecipe>();

    void Awake()
    {
        BuildRecipeDatabase();
    }

    public int GetItemFromRecipe(int[] recipe)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipe.SequenceEqual(recipes[i].requiredItems))
            {
                return recipes[i].itemToCraft;
            }
        }
        return 0;
        
    }

   void BuildRecipeDatabase()
    {
        recipes = new List<CraftRecipe>()
        {
            new CraftRecipe( new int[] {1}, 2),

            new CraftRecipe( new int[] {1,1}, 3),

            new CraftRecipe( new int[] {1,4}, 7),

            new CraftRecipe( new int[] {4}, 5),

            new CraftRecipe( new int[] {4,4}, 6),
        };
    }
}
