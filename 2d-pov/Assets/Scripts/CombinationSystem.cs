using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CombinationSystem : MonoBehaviour
{
    public Transform plate; // Reference to the Plate/Dish GameObject
    public List<Recipe> allRecipes = new List<Recipe>(); // Assign in Inspector

    // Call this whenever ingredients are added to the plate
    public void CheckForCombinations()
    {
        // Get all ingredient children on the plate
        List<GameObject> ingredientObjects = new List<GameObject>();
        List<string> ingredientNames = new List<string>();

        foreach (Transform child in plate)
        {
            Ingredient ing = child.GetComponent<Ingredient>();
            if (ing != null)
            {
                ingredientObjects.Add(child.gameObject);
                string name = ing.getIngredientName;
                ingredientNames.Add(name);
            }
        }

        // Need at least 2 ingredients to combine
        if (ingredientNames.Count < 2)
            return;

        // Check all recipes to find a match
        foreach (Recipe recipe in allRecipes)
        {
            if (recipe.MatchesIngredients(ingredientNames))
            {
                Debug.Log("Recipe found: " + recipe.dishName);
                CombineIntoDish(ingredientObjects, recipe);
                return; // Stop after first match
            }
        }

        Debug.Log("No matching recipe found for these ingredients");
    }

    void CombineIntoDish(List<GameObject> ingredientsToRemove, Recipe recipe)
    {
        // Remove all ingredient GameObjects
        foreach (GameObject ing in ingredientsToRemove)
        {
            Destroy(ing);
        }

        // Spawn the combined dish
        if (recipe.dishPrefab != null)
        {
            GameObject dish = Instantiate(recipe.dishPrefab, plate.position, Quaternion.identity);
            dish.transform.SetParent(plate);
            dish.transform.localPosition = Vector3.zero;

            Debug.Log("Created dish: " + recipe.dishName);
        }
        else
        {
            Debug.LogWarning("Recipe " + recipe.dishName + " has no dish prefab assigned!");
        }
    }

    // Alternative: Check for specific number of ingredients
    public void CheckForCombinationsWithCount(int minIngredients)
    {
        List<GameObject> ingredientObjects = new List<GameObject>();
        List<string> ingredientNames = new List<string>();

        foreach (Transform child in plate)
        {
            Ingredient ing = child.GetComponent<Ingredient>();
            if (ing != null)
            {
                ingredientObjects.Add(child.gameObject);
                ingredientNames.Add(ing.getIngredientName);
            }
        }

        if (ingredientNames.Count < minIngredients)
            return;

        // Try to find exact matches first
        foreach (Recipe recipe in allRecipes)
        {
            if (recipe.MatchesIngredients(ingredientNames))
            {
                CombineIntoDish(ingredientObjects, recipe);
                return;
            }
        }
    }
}
