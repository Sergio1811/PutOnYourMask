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
            new CraftRecipe( new int[] {1,2}, 5),
            new CraftRecipe( new int[] {2,1}, 5),

            new CraftRecipe( new int[] {2,3}, 6),
            new CraftRecipe( new int[] {3,2}, 6),

            new CraftRecipe( new int[] {1,3}, 4),
            new CraftRecipe( new int[] {3,1}, 4),

            new CraftRecipe (new int[] {1}, 7),
            new CraftRecipe (new int[] {2}, 8),
            new CraftRecipe (new int[] {3}, 9),
            new CraftRecipe (new int[] {4}, 10),
            new CraftRecipe (new int[] {5}, 11),
            new CraftRecipe (new int[] {6}, 12)

        };
    }
}
