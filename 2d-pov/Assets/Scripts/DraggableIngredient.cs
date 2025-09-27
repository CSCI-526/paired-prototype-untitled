using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    [Header("Drag Settings")]
    public LayerMask plateLayerMask = -1; // What layers count as valid drop zones
    public float dragOffset = 0.1f; // How far in front of camera to place while dragging
    
    private Camera mainCamera;
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Collider2D col2D;
    private Rigidbody2D rb2D;
    private int originalSortingOrder;
    private SpriteRenderer spriteRenderer;
    
    // Events for other systems to hook into
    public System.Action<DraggableIngredient> OnStartDrag;
    public System.Action<DraggableIngredient> OnEndDrag;
    public System.Action<DraggableIngredient, Plate> OnDroppedOnPlate;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            mainCamera = FindObjectOfType<Camera>();
            
        originalPosition = transform.position;
        col2D = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
            originalSortingOrder = spriteRenderer.sortingOrder;
    }

    void OnMouseDown()
    {
        if (!enabled) return;
        
        StartDragging();
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane + dragOffset;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        
        // For 2D games, keep the Z position consistent
        worldPos.z = transform.position.z;
        transform.position = worldPos;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        
        StopDragging();
    }

    void StartDragging()
    {
        isDragging = true;
        
        // Disable physics while dragging
        if (rb2D != null)
            rb2D.isKinematic = true;
            
        // Bring to front while dragging
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = 100;
            
        OnStartDrag?.Invoke(this);
    }

    void StopDragging()
    {
        isDragging = false;
        
        // Re-enable physics
        if (rb2D != null)
            rb2D.isKinematic = false;
            
        // Reset sorting order
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = originalSortingOrder;
        
        // Check if we're over a valid drop zone
        Plate plateBelow = GetPlateBelow();
        
        if (plateBelow != null)
        {
            // Successfully dropped on plate
            plateBelow.AddIngredient(this);
            OnDroppedOnPlate?.Invoke(this, plateBelow);
        }
        else
        {
            // Return to original position if not dropped on plate
            ReturnToOriginalPosition();
        }
        
        OnEndDrag?.Invoke(this);
    }

    Plate GetPlateBelow()
    {
        // Raycast downward to find plates
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0f, plateLayerMask);
        
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Plate>();
        }
        
        // Alternative method using OverlapPoint
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position, plateLayerMask);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Don't detect ourselves
            {
                Plate plate = collider.GetComponent<Plate>();
                if (plate != null)
                    return plate;
            }
        }
        
        return null;
    }

    void ReturnToOriginalPosition()
    {
        // Smoothly return to original position
        StartCoroutine(ReturnToPositionCoroutine());
    }
    
    System.Collections.IEnumerator ReturnToPositionCoroutine()
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        float duration = 0.3f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Use easing for smooth return
            t = 1f - (1f - t) * (1f - t); // Ease out quad
            
            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            yield return null;
        }
        
        transform.position = originalPosition;
    }
    
    // Call this if you want to reset the ingredient's original position
    public void SetNewOriginalPosition()
    {
        originalPosition = transform.position;
    }
    
    // Disable dragging (useful for ingredients already on plate)
    public void DisableDragging()
    {
        enabled = false;
    }
    
    public void EnableDragging()
    {
        enabled = true;
    }
}