using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RescaleUIColliders : MonoBehaviour
{
    // Reference to the RectTransform of the UI element
    private RectTransform rectTransform;

    // Reference to the BoxCollider2D attached to this UI element
    private BoxCollider2D boxCollider2D;

    private Vector2 lastScreenSize;

    void Start()
    {
        // Get the RectTransform of the UI element
        rectTransform = GetComponent<RectTransform>();

        // Get the BoxCollider2D attached to the UI element
        boxCollider2D = GetComponent<BoxCollider2D>();

        // Initialize the last screen size
        lastScreenSize = new Vector2(Screen.width, Screen.height);

        // Initially scale the collider to match the UI element's size
        RescaleCollider();
    }

    void Update()
    {
        // Detect if the screen size has changed (window resize or resolution change)
        if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
        {
            // Update the last screen size
            lastScreenSize = new Vector2(Screen.width, Screen.height);

            // Rescale the collider based on the new screen size
            RescaleCollider();
        }
        RescaleCollider();
    }

    // Method to rescale the BoxCollider2D to match the UI element's size
    void RescaleCollider()
    {
        if (rectTransform != null && boxCollider2D != null)
        {
            // Get the width and height of the UI element's RectTransform in world units
            Vector2 newSize = rectTransform.rect.size;

            // Scale the BoxCollider2D to match the UI element
            boxCollider2D.size = newSize;

            // Optionally adjust the offset to match the position of the RectTransform
            boxCollider2D.offset = rectTransform.rect.center;

           // Debug.Log("Resized BoxCollider2D to: " + newSize);
        }
    }
}
