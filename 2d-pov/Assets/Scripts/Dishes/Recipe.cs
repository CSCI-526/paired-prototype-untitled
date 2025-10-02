
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ScriptableObject to define a recipe
[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public string dishName;
    public string ID;
    public List<string> requiredIngredients = new List<string>();
    public GameObject dishPrefab; // The final food item to spawn

    public Sprite icon;

    // Check if the given ingredients match this recipe
    public bool MatchesIngredients(List<string> ingredients)
    {
        if (ingredients.Count != requiredIngredients.Count)
            return false;

        // Sort both lists to compare regardless of order
        List<string> sortedIngredients = new List<string>(ingredients);
        List<string> sortedRequired = new List<string>(requiredIngredients);
        sortedIngredients.Sort();
        sortedRequired.Sort();

        // Check if all ingredients match
        for (int i = 0; i < sortedIngredients.Count; i++)
        {
            if (sortedIngredients[i] != sortedRequired[i])
                return false;
        }

        return true;
    }
}