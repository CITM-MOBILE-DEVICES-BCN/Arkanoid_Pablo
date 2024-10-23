using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.levelscore;
      
    }
}
