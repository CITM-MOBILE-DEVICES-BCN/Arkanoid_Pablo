using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelscompleted : MonoBehaviour
{
   
    
    void Start()
    {
      
    }

    public IEnumerator LoadLevel(string Scenename)
    {
        yield return new WaitForSeconds(2);
        SceneManager.Instance.LoadScene(Scenename);
    }

    void Update()
    {
        switch(ScoreManager.Instance.level %2)
        {
            case 0:
                StartCoroutine(LoadLevel("Level2"));
                Savegame.Instance.SaveGame();


                break;
            case 1:
                StartCoroutine(LoadLevel("Level1"));
                Savegame.Instance.SaveGame();
                break;
        }

    }
}
