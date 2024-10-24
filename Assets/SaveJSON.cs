using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveJSON : MonoBehaviour
{
    [System.Serializable]

    public class PlayerData
    {
        public int currentScore;
        public int highScore;
        public int lives;
        public string scena;
        public int scena2;
    }

    string comprobar;
    string filePath;
    private void Awake()
    {
        filePath = Application.dataPath + "/SavedFiles/setting.json";

        // Crea la carpeta "SavedFiles" si no existe
        if (!System.IO.Directory.Exists(Application.dataPath + "/SavedFiles"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/SavedFiles");
        }
    }

    public PlayerData facts;
    public void SerializePlayerData()
    {

        facts.highScore = FindObjectOfType<ScoreManager>().highscore;
        facts.currentScore = FindObjectOfType<ScoreManager>().levelscore;
        facts.lives = FindObjectOfType<ScoreManager>().lives;
        if(FindObjectOfType<ScoreManager>().level == 1)
        {
             facts.scena = "Level1";
        }
        else
        {
            facts.scena = "Level2";
        }
        


        JsonUtility.ToJson(facts);
        string json = JsonUtility.ToJson(facts);

        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        System.IO.File.WriteAllText(filePath, json);

        comprobar = loadedData.currentScore + " " + loadedData.highScore + " " + loadedData.scena + " " + loadedData.lives;
        

    }

    public void DeSerializePlayerData()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);
            // Verificar si el archivo existe
            SceneManager.Instance.LoadScene(loadedData.scena);

            ScoreManager gameManager = FindObjectOfType<ScoreManager>();
            if (gameManager != null)
            {
                gameManager.highscore = loadedData.highScore;
                gameManager.levelscore = loadedData.currentScore;
                gameManager.lives = loadedData.lives;
            }
            Time.timeScale = 1.0f;
        }
    }


}

