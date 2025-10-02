using UnityEngine;
<<<<<<< HEAD
using System.Collections.Generic;
public class Dish : MonoBehaviour
{
    public Recipe recipe; // The recipe this dish corresponds to

=======

[ExecuteInEditMode] // run in edit mode to update visuals
[RequireComponent(typeof(SpriteRenderer))] // ensure there's a SpriteRenderer
public class Dish : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Recipe recipe;
    void OnValidate()
    {
        ApplyData();
    }

    void ApplyData()
    {
        if (recipe != null && recipe.icon != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = recipe.icon;
                gameObject.name = recipe.dishName;
            }
        }
    }

>>>>>>> 86d4737dce1583023ece01183e5872db1d21e8ea
}
