using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int lives=3;
    public  int score = 0;    // The current score
    public  int levelscore = 0;
    public int level = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }
    }
    private void Start()
    {
        // Initialize the score display
        UpdateScoreUI();
    }

    // Method to add score and update the UI
    public  void AddScore(int amount)
    {
        levelscore += 300;
        score += amount;
        // Update the score on the UI
        FindObjectOfType<ScoreManager>().UpdateScoreUI();
    }

    // Update the score in the UI
    public void UpdateScoreUI()
    {
        
       if( score % 4500 == 0 && score >0 )
        {
            level++;
            SceneManager.Instance.LoadScene("Level Completed");
            AddScore(-4500);
        }
        
       
    }
}


