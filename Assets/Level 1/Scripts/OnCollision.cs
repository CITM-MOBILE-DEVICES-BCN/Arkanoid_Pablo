using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{

    // Reference to the 2D collider of the object we want to detect
    public Collider2D target2DCollider;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the UI element is overlapping with the 2D collider
        if (collision.gameObject.CompareTag("Player"))
        {
            // Increase the score by 300 points
            ScoreManager.Instance.AddScore(300);

            Musicmanager.instance.PlaySound("Pong");
            // Destroy this UI object (the Image component or the entire GameObject)
            Destroy(gameObject);
        }
    }
}


