using UnityEngine;
using System.Collections.Generic;

public class RatingSystem : MonoBehaviour
{
    public Plate playerPlate;
    public OrderDish orderDish;

    public void SubmitDish()
    {
        int stars = CalculateRating();
        Debug.Log("Rating: " + stars + " Stars!");

        // You can add visual feedback here later
        DisplayStars(stars);
    }

    int CalculateRating()
    {
        if (orderDish == null || playerPlate == null)
        {
            Debug.LogError("No order dish or player plate provided for rating!");
            return 1;
        }

        if (playerPlate.IsEmpty())
        {
            Debug.Log("Plate is empty!");
            return 1;
        }

        if (orderDish.requiredIngredients.Count == 0)
        {
            Debug.LogWarning("Order has no required ingredients!");
            return 1;
        }

        // Get ingredient names from the plate
        List<string> plateIngredients = new List<string>(playerPlate.GetIngredientNames());
        List<string> requiredIngredients = new List<string>(orderDish.requiredIngredients);

        // Check if counts match
        if (plateIngredients.Count != requiredIngredients.Count)
        {
            Debug.Log("Wrong number of ingredients!");
            return 1;
        }

        // Sort both lists for comparison
        plateIngredients.Sort();
        requiredIngredients.Sort();

        // Check if all ingredients match
        for (int i = 0; i < plateIngredients.Count; i++)
        {
            if (plateIngredients[i] != requiredIngredients[i])
            {
                Debug.Log("Ingredient mismatch: Expected " + requiredIngredients[i] + " but got " + plateIngredients[i]);
                return 1;
            }
        }

        // Perfect match!
        Debug.Log("Perfect match!");
        return 5;
    }

    void DisplayStars(int stars)
    {
        string starDisplay = "";
        for (int i = 0; i < stars; i++)
        {
            starDisplay += "★ ";
        }
        Debug.Log(starDisplay);
    }
}