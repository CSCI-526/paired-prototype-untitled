using UnityEngine;

public class PantrySlot : MonoBehaviour
{
    [Header("Slot Configuration")]
    public GameObject ingredientPrefab; // Assign the prefab in inspector
    
    private Pantry pantry;
    private GameObject currentIngredient;

    // private void Start()
    // {
    //     // Find pantry if not set
    //     if (pantry == null)
    //         pantry = GetComponentInParent<Pantry>();
            
    //     // Spawn initial ingredient if we have a prefab
    //     if (ingredientPrefab != null && currentIngredient == null)
    //     {
    //         SpawnIngredient();
    //     }
    // }

    // public void SetPantry(Pantry pantryRef)
    // {
    //     pantry = pantryRef;
    // }

    // public void SetCurrentIngredient(GameObject ingredient)
    // {
    //     currentIngredient = ingredient;
    // }
    
    // public void SetIngredientPrefab(GameObject prefab)
    // {
    //     ingredientPrefab = prefab;
    // }

    // void SpawnIngredient()
    // {
    //     if (ingredientPrefab != null && pantry != null)
    //     {
    //         // Validate position before spawning
    //         Vector3 spawnPosition = transform.position;
            
    //         // Check for invalid values
    //         if (!IsValidPosition(spawnPosition))
    //         {
    //             Debug.LogError($"[PantrySlot] Invalid spawn position detected: {spawnPosition}. Using Vector3.zero instead.");
    //             spawnPosition = Vector3.zero;
    //         }
            
    //         currentIngredient = Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity, transform);
            
    //         if (pantry.enableDebugLogs)
    //             Debug.Log($"[PantrySlot] Spawned {ingredientPrefab.name} in slot {gameObject.name} at position {spawnPosition}");
    //     }
    // }
    
    // bool IsValidPosition(Vector3 position)
    // {
    //     return !float.IsNaN(position.x) && !float.IsNaN(position.y) && !float.IsNaN(position.z) &&
    //            !float.IsInfinity(position.x) && !float.IsInfinity(position.y) && !float.IsInfinity(position.z) &&
    //            Mathf.Abs(position.x) < 1000000f && Mathf.Abs(position.y) < 1000000f && Mathf.Abs(position.z) < 1000000f;
    // }

    // private void OnTransformChildrenChanged()
    // {
    //     // Check if ingredient got deleted/removed
    //     if (currentIngredient == null && transform.childCount == 0 && ingredientPrefab != null)
    //     {
    //         if (pantry != null && pantry.enableDebugLogs)
    //             Debug.Log($"[PantrySlot] Ingredient missing, respawning {ingredientPrefab.name}");
                
    //         SpawnIngredient();
    //     }
    // }
}
