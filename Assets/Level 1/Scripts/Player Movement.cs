using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;

    private bool isAutoMovement = true;

    public float autoMoveSpeed = 5f;

    public Button toggleMovementButton;

    public RectTransform canvasRectTransform;

    private RectTransform playerRectTransform;

    private int direction = 1;

    void Start()
    {
        cam = Camera.main;
        playerRectTransform = GetComponent<RectTransform>();

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
            AutomaticMovement();
        }
        else
        {
            MouseBasedMovement();
        }
    }

    private void AutomaticMovement()
    {
        playerRectTransform.anchoredPosition += Vector2.right * autoMoveSpeed * Time.deltaTime * direction;

        ClampPlayerPosition();
    }

    private void MouseBasedMovement()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, cam, out localMousePosition);

        playerRectTransform.anchoredPosition = new Vector2(localMousePosition.x, playerRectTransform.anchoredPosition.y);

        ClampPlayerPosition();
    }

    private void ToggleMovementMode()
    {
        isAutoMovement = !isAutoMovement;

        Debug.Log("Movement mode: " + (isAutoMovement ? "Automatic" : "Mouse-based"));
    }

    private void ClampPlayerPosition()
    {
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        float playerHalfWidth = playerRectTransform.rect.width / 2;

        if (playerRectTransform.anchoredPosition.x - playerHalfWidth <= -canvasSize.x / 2)
        {
            direction = 1;
            playerRectTransform.anchoredPosition = new Vector2(-canvasSize.x / 2 + playerHalfWidth, playerRectTransform.anchoredPosition.y);
        }
        else if (playerRectTransform.anchoredPosition.x + playerHalfWidth >= canvasSize.x / 2)
        {
            direction = -1;
            playerRectTransform.anchoredPosition = new Vector2(canvasSize.x / 2 - playerHalfWidth, playerRectTransform.anchoredPosition.y);
        }
    }
}
