using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText; // Reference to the TextMeshProUGUI component
    
    void Start()
    {

    }

    void Update()
    {

        HighscoreText.text = "HighScore: " + PlayerPrefs.GetInt("Highscore",ScoreManager.Instance.highscore);

    }
}
