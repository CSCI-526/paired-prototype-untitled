using UnityEngine;
using System.Collections.Generic;

public class Plate : MonoBehaviour
{
    [Header("Plate Settings")]
    public Transform ingredientParent; // Where ingredients will be parented (optional)
    public float maxIngredients = 10f;
    public bool allowDuplicates = true;
    public float ingredientSpacing = 0.2f;
    
    [Header("Visual Feedback")]
    public GameObject highlightEffect; // Optional highlight when hovering
    public Color highlightColor = Color.yellow;
    
    
    private List<DraggableIngredient> ingredientsOnPlate = new List<DraggableIngredient>();
    private SpriteRenderer plateRenderer;
    private Color originalColor;
    
    // Events
    public System.Action<DraggableIngredient> OnIngredientAdded;
    public System.Action<DraggableIngredient> OnIngredientRemoved;
    public System.Action OnPlateFull;
    public System.Action OnPlateEmpty;

    void Start()
    {
        plateRenderer = GetComponent<SpriteRenderer>();
        if (plateRenderer != null)
            originalColor = plateRenderer.color;
            
        // If no ingredient parent specified, use this transform
        if (ingredientParent == null)
            ingredientParent = transform;
            
        if (highlightEffect != null)
            highlightEffect.SetActive(false);
    }

    public bool AddIngredient(DraggableIngredient ingredient)
    {
        // Check if we can add this ingredient
        if (!CanAddIngredient(ingredient))
            return false;
        
        // Add to our list
        ingredientsOnPlate.Add(ingredient);
        
        // Position the ingredient on the plate
        PositionIngredientOnPlate(ingredient);
        
        // Parent the ingredient to the plate (optional)
        if (ingredientParent != null)
            ingredient.transform.SetParent(ingredientParent);
        
        
        // Update ingredient's original position so it doesn't return to old spot
        // ingredient.SetNewOriginalPosition();
        
        // Trigger events
        OnIngredientAdded?.Invoke(ingredient);
        
        if (IsFull())
            OnPlateFull?.Invoke();
        
        Debug.Log($"Added {ingredient.name} to plate. Total ingredients: {ingredientsOnPlate.Count}");
        return true;
    }

    public bool RemoveIngredient(DraggableIngredient ingredient)
    {
        if (!ingredientsOnPlate.Contains(ingredient))
            return false;
        
        bool wasEmpty = IsEmpty();
        
        ingredientsOnPlate.Remove(ingredient);
        
        // Unparent and re-enable dragging
        ingredient.transform.SetParent(null);
        ingredient.EnableDragging();
        
        // Reposition remaining ingredients
        RepositionIngredients();
        
        OnIngredientRemoved?.Invoke(ingredient);
        
        if (!wasEmpty && IsEmpty())
            OnPlateEmpty?.Invoke();
        
        Debug.Log($"Removed {ingredient.name} from plate. Total ingredients: {ingredientsOnPlate.Count}");
        return true;
    }

    bool CanAddIngredient(DraggableIngredient ingredient)
    {
        // Check if plate is full
        if (IsFull())
        {
            Debug.Log("Plate is full!");
            return false;
        }
        
        // Check for duplicates if not allowed
        if (!allowDuplicates)
        {
            foreach (DraggableIngredient existing in ingredientsOnPlate)
            {
                if (existing.name == ingredient.name || 
                    existing.gameObject.name == ingredient.gameObject.name)
                {
                    Debug.Log("Duplicate ingredient not allowed!");
                    return false;
                }
            }
        }
        
        return true;
    }

    void PositionIngredientOnPlate(DraggableIngredient ingredient)
    {
        // Simple positioning in a grid or circle pattern
        Vector3 plateCenter = transform.position;
        int index = ingredientsOnPlate.Count - 1; // -1 because we already added to list
        
        // Spiral positioning
        float angle = index * 60f * Mathf.Deg2Rad; // 60 degrees apart
        float radius = 0.3f + (index / 6f) * 0.2f; // Increase radius for outer rings
        
        Vector3 offset = new Vector3(
            Mathf.Cos(angle) * radius,
            Mathf.Sin(angle) * radius,
            -0.1f // Slightly in front of plate
        );
        
        ingredient.transform.position = plateCenter + offset;
        
        // You could also do a simple grid:
        /*
        int columns = 3;
        int row = index / columns;
        int col = index % columns;
        
        Vector3 gridOffset = new Vector3(
            (col - 1) * ingredientSpacing,
            (row - 1) * ingredientSpacing,
            -0.1f
        );
        
        ingredient.transform.position = plateCenter + gridOffset;
        */
    }

    void RepositionIngredients()
    {
        for (int i = 0; i < ingredientsOnPlate.Count; i++)
        {
            if (ingredientsOnPlate[i] != null)
            {
                // Temporarily add to list position for repositioning calculation
                var temp = ingredientsOnPlate[i];
                ingredientsOnPlate.RemoveAt(i);
                ingredientsOnPlate.Insert(i, temp);
                
                // Reposition based on new index
                Vector3 plateCenter = transform.position;
                float angle = i * 60f * Mathf.Deg2Rad;
                float radius = 0.3f + (i / 6f) * 0.2f;
                
                Vector3 offset = new Vector3(
                    Mathf.Cos(angle) * radius,
                    Mathf.Sin(angle) * radius,
                    -0.1f
                );
                
                ingredientsOnPlate[i].transform.position = plateCenter + offset;
                ingredientsOnPlate[i].SetNewOriginalPosition();
            }
        }
    }

    // Visual feedback methods
    void OnMouseEnter()
    {
        ShowHighlight();
    }

    void OnMouseExit()
    {
        HideHighlight();
    }

    void ShowHighlight()
    {
        if (highlightEffect != null)
            highlightEffect.SetActive(true);
        else if (plateRenderer != null)
            plateRenderer.color = highlightColor;
    }

    void HideHighlight()
    {
        if (highlightEffect != null)
            highlightEffect.SetActive(false);
        else if (plateRenderer != null)
            plateRenderer.color = originalColor;
    }

    // Utility methods
    public bool IsFull() => ingredientsOnPlate.Count >= maxIngredients;
    public bool IsEmpty() => ingredientsOnPlate.Count == 0;
    public int GetIngredientCount() => ingredientsOnPlate.Count;
    public List<DraggableIngredient> GetIngredients() => new List<DraggableIngredient>(ingredientsOnPlate);
    
    // Button to reset the dish
    public void ClearPlate()
    {
        while (ingredientsOnPlate.Count > 0)
        {
            RemoveIngredient(ingredientsOnPlate[0]);
        }
    }
    
    // Get recipe/dish information
    public List<string> GetIngredientNames()
    {
        List<string> names = new List<string>();
        foreach (DraggableIngredient ingredient in ingredientsOnPlate)
        {
            names.Add(ingredient.name);
        }
        return names;
    }
}