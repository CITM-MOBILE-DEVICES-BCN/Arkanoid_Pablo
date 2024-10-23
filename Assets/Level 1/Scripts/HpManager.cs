using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour
{
    
    public Image[] hpImages; // Array of images for the HP (set in Inspector)
    private int maxHP = 3;


    void Start()
    {
        // Initialize HP to the max value
        UpdateHPUI();
    }

    // Method to update the HP displayed in the UI
    public void UpdateHPUI()
    {
       

        for (int i = 0; i < hpImages.Length; i++)
        {
            if (i < ScoreManager.Instance.lives)
            {
                hpImages[i].enabled = true; // Show HP image
            }
            else
            {
                hpImages[i].enabled = false; // Hide HP image
            }
        }
        if(IsHPDepleted())
        {
            // Game over
            Debug.Log("Game Over");
            SceneManager.Instance.LoadScene("Game Over");
        }
    }

    // Method to reset HP to the maximum value
    public void ResetHP()
    {
        ScoreManager.Instance.lives = maxHP;
        UpdateHPUI();
    }

    // Method to decrease HP
    public void DecreaseHP()
    {
        ScoreManager.Instance.lives--;
        UpdateHPUI();
    }

    // Method to check if HP is depleted
    public bool IsHPDepleted()
    {
        return ScoreManager.Instance.lives <= 0;
    }
    public void IncreseHP()
    {
        ScoreManager.Instance.lives++;
        UpdateHPUI();
    }
}
