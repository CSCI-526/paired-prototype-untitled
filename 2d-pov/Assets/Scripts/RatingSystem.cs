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

        // // Get ingredient names from the plate
        // List<string> plateIngredients = new List<string>(playerPlate.GetIngredientNames());
        // List<string> orderDishes = new List<string>(orderDish.dishes);

        // // Check if counts match
        // if (plateIngredients.Count != orderDishes.Count)
        // {
        //     Debug.Log("Wrong number of ingredients!");
        //     return 1;
        // }

        // // Sort both lists for comparison
        // plateIngredients.Sort();
        // orderDishes.Sort();

        // // Check if all ingredients match
        // for (int i = 0; i < plateIngredients.Count; i++)
        // {
        //     if (plateIngredients[i] != orderDishes[i])
        //     {
        //         Debug.Log("Ingredient mismatch: Expected " + orderDishes[i] + " but got " + plateIngredients[i]);
        //         return 1;
        //     }
        // }

        // check if the child component game object under playerPlate has the same dishName as one of the orderDish.dishes
        string plateDishName = playerPlate.getDishName();
        string orderDishName = orderDish.orderName;

        if (plateDishName == orderDishName)
        {
            Debug.Log("Dish names match!");
            return 5;
        }

        return 10; // Partial match
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