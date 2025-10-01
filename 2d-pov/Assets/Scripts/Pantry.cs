using UnityEngine;
using System.Collections.Generic;

public class Pantry : MonoBehaviour
{
    [Header("Pantry Settings")]
    public Transform ingredientParent; // Where ingredients will be parented (optional)
    public bool allowDuplicates = true;

    [Header("Visual Feedback")]
    public GameObject highlightEffect; // Optional highlight when hovering
    public Color highlightColor = Color.green;

    private List<DraggableIngredient> ingredientsOnPlate = new List<DraggableIngredient>();
    private SpriteRenderer plateRenderer;
    private Color originalColor;

    // Events
    public System.Action<DraggableIngredient> OnIngredientAdded;
    public System.Action<DraggableIngredient> OnIngredientRemoved;
    
    
    // Call remove ingredient when the ingredient is dropped outside the pantry
}
