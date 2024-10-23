using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision2 : MonoBehaviour
{
    // Reference to the 2D collider of the object we want to detect
    public Collider2D target2DCollider;

    // Number of collisions needed to delete the UI element
    public int requiredCollisions = 3;

    // Internal counter to track how many collisions have occurred
    private int collisionCount = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the UI element is overlapping with the 2D collider
        if (collision.gameObject.CompareTag("Player"))  
        {
            collisionCount++;
            Musicmanager.instance.PlaySound("Pong");
            if (collisionCount >= requiredCollisions)
            {
                // Increase the score by 300 points
                 ScoreManager.Instance.AddScore(300);

                // Destroy this UI object (the Image component or the entire GameObject)
                Destroy(gameObject);

            }
           
        }
    }
}