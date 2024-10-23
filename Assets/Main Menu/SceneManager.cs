using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{

    // Singleton instance
    public static SceneManager Instance { get; private set; }

    // Awake ensures the singleton pattern and applies DontDestroyOnLoad
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

    
    // Load scene by name synchronously
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
   
}
