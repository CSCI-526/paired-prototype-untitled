using UnityEngine;


[ExecuteInEditMode] // run in edit mode to update visuals
[RequireComponent(typeof(SpriteRenderer))] // ensure there's a SpriteRenderer
// [RequireComponent(typeof(PolygonCollider2D))] // ensure there's a Collider2D
public class Ingredient : MonoBehaviour
{
    public IngredientData ingredientData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnValidate()
    {
        ApplyData();
    }

    void ApplyData()
    {
        if (ingredientData != null && ingredientData.icon != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = ingredientData.icon;
                gameObject.name = ingredientData.ingredientName;
            }
        }
    }
}
