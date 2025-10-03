using UnityEngine;

public class Pantry : MonoBehaviour
{
    [Header("Pantry Settings")]
    public bool enableDebugLogs = true;
    
    [Header("Ingredient Prefabs")]
    public GameObject[] ingredientPrefabs; // Array of prefabs to assign to slots

    // private void Start()
    // {
    //     SetupPantrySlots();
    // }

    // void SetupPantrySlots()
    // {
    //     PantrySlot[] slots = GetComponentsInChildren<PantrySlot>();
        
    //     if (enableDebugLogs)
    //         Debug.Log($"[Pantry] Found {slots.Length} PantrySlots");

    //     // Ensure every child has a PantrySlot attached
    //     foreach (Transform child in transform)
    //     {
    //         PantrySlot slot = child.GetComponent<PantrySlot>();
    //         if (slot == null)
    //         {
    //             slot = child.gameObject.AddComponent<PantrySlot>();
    //             if (enableDebugLogs)
    //                 Debug.Log($"[Pantry] Added PantrySlot to {child.name}");
    //         }
            
    //         slot.SetPantry(this);
    //     }
        
    //     // Auto-assign prefabs to slots if they don't have them
    //     AssignPrefabsToSlots();
    // }
    
    // void AssignPrefabsToSlots()
    // {
    //     if (ingredientPrefabs == null || ingredientPrefabs.Length == 0)
    //     {
    //         if (enableDebugLogs)
    //             Debug.LogWarning("[Pantry] No ingredient prefabs assigned!");
    //         return;
    //     }
        
    //     PantrySlot[] slots = GetComponentsInChildren<PantrySlot>();
        
    //     for (int i = 0; i < slots.Length; i++)
    //     {
    //         // Only assign if slot doesn't already have a prefab
    //         if (slots[i].ingredientPrefab == null)
    //         {
    //             // Cycle through prefabs if we have more slots than prefabs
    //             int prefabIndex = i % ingredientPrefabs.Length;
    //             slots[i].SetIngredientPrefab(ingredientPrefabs[prefabIndex]);
                
    //             if (enableDebugLogs)
    //                 Debug.Log($"[Pantry] Assigned {ingredientPrefabs[prefabIndex].name} to slot {slots[i].name}");
    //         }
    //     }
    // }

    // /// <summary>
    // /// Called by PantrySlot when its ingredient is destroyed.
    // /// </summary>
    // public void RespawnIngredient(GameObject prefab, Vector3 position, Transform parentSlot)
    // {
    //     GameObject newIngredient = Instantiate(prefab, position, Quaternion.identity, parentSlot);

    //     // Reattach PantrySlot watcher
    //     PantrySlot slot = parentSlot.GetComponent<PantrySlot>();
    //     if (slot != null) slot.SetCurrentIngredient(newIngredient);

    //     if (enableDebugLogs)
    //         Debug.Log($"[Pantry] Respawned {prefab.name} at {position}");
    // }
    
    // /// <summary>
    // /// Manually refresh all slots (useful for testing)
    // /// </summary>
    // [ContextMenu("Refresh Pantry Slots")]
    // public void RefreshSlots()
    // {
    //     SetupPantrySlots();
    // }
}
