using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText; // Reference to the TextMeshProUGUI component
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        HighscoreText.text = "Highcore: " + ScoreManager.Instance.levelscore;

    }
}
