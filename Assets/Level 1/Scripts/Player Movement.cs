using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    // Boolean to track which movement mode is active
    private bool isAutoMovement = true;

    // Speed for automatic movement
    public float autoMoveSpeed = 5f;

    // Reference to the UI button
    public Button toggleMovementButton;

    // Reference to the canvas' RectTransform
    public RectTransform canvasRectTransform;

    // Player's RectTransform (since it's a UI element)
    private RectTransform playerRectTransform;

    // Movement direction, 1 for right and -1 for left
    private int direction = 1;

    void Start()
    {
        // Get the camera and player's RectTransform
        cam = Camera.main;
        playerRectTransform = GetComponent<RectTransform>();

        // Set up the button listener to toggle movement modes
        if (toggleMovementButton != null)
        {
            toggleMovementButton.onClick.AddListener(ToggleMovementMode);
        }
        else
        {
            Debug.LogError("Toggle Movement Button not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (isAutoMovement)
        {
            // Automatic movement (left and right with boundary check)
            AutomaticMovement();
        }
        else
        {
            // Mouse-based movement
            MouseBasedMovement();
        }
    }

    // Function for automatic left-right movement with direction change
    private void AutomaticMovement()
    {
        // Move the player based on the direction and speed
        playerRectTransform.anchoredPosition += Vector2.right * autoMoveSpeed * Time.deltaTime * direction;

        // Check if the player has reached the limits and switch direction if needed
        ClampPlayerPosition();
    }

    // Function for mouse-based movement
    private void MouseBasedMovement()
    {
        // Get the mouse position in world space and convert it to local canvas space
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, cam, out localMousePosition);

        // Update the player's X position, keeping the current Y position
        playerRectTransform.anchoredPosition = new Vector2(localMousePosition.x, playerRectTransform.anchoredPosition.y);

        // Clamp the movement within canvas bounds
        ClampPlayerPosition();
    }

    // This function toggles between automatic and mouse movement
    private void ToggleMovementMode()
    {
        isAutoMovement = !isAutoMovement;

        // Optionally, you can log the current mode to the console
        Debug.Log("Movement mode: " + (isAutoMovement ? "Automatic" : "Mouse-based"));
    }

    // This function clamps the player's position within the canvas bounds and changes direction if needed
    private void ClampPlayerPosition()
    {
        // Get the canvas bounds
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // Get the player's width to prevent it from going off-screen
        float playerHalfWidth = playerRectTransform.rect.width / 2;

        // Check for boundary collision and reverse direction if the player reaches the edges
        if (playerRectTransform.anchoredPosition.x - playerHalfWidth <= -canvasSize.x / 2)
        {
            // Hit the left edge, change direction to right
            direction = 1;
            playerRectTransform.anchoredPosition = new Vector2(-canvasSize.x / 2 + playerHalfWidth, playerRectTransform.anchoredPosition.y);
        }
        else if (playerRectTransform.anchoredPosition.x + playerHalfWidth >= canvasSize.x / 2)
        {
            // Hit the right edge, change direction to left
            direction = -1;
            playerRectTransform.anchoredPosition = new Vector2(canvasSize.x / 2 - playerHalfWidth, playerRectTransform.anchoredPosition.y);
        }
    }
}
