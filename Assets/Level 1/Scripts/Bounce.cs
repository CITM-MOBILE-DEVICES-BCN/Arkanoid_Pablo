using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    public float speed = 1f; // Initial speed of the object
    public Vector2 initialPosition = new Vector2(0, 0); // Initial position of the object
    private Rigidbody2D rb;
    private Vector2 lastVelocity; // Store the object's velocity before collision
    private bool firstImpact = true; // Tracks whether the first impact has occurred

    // Reference to HP UI Manager
    public HpManager hpUIManager;

    // Power-up multiplier for speed
    public float powerUpMultiplier = 1.5f; // Multiplies speed by 1.5 when power-up is collected

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartState(); // Set the object to its initial state at the start of the game
    }

    void Update()
    {
        // Store the current velocity of the object
        lastVelocity = rb.velocity;

        // Check if the object's Y position is less than -5
        if (transform.position.y <= -5f)
        {
            Debug.Log("Object went out of bounds! Resetting...");
            ResetObjectState(); // Reset object to initial state if it goes below Y = -5
        }
    }

    // Coroutine to apply initial velocity after a delay
    IEnumerator ApplyInitialVelocityAfterDelay(float delay)
    {
        // Wait for the specified delay (2 seconds)
        yield return new WaitForSeconds(delay);

        // After the delay, set the initial velocity with an X velocity equal to 'speed' and Y velocity of -0.4
        rb.velocity = new Vector2(speed, -0.4f);

        Debug.Log("Initial velocity applied: " + rb.velocity);
    }

    // This method is called when the object collides with something
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate the new direction by reflecting the velocity off the collision normal
        Vector2 newDirection = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        // Increase the speed based on whether it's the first impact or subsequent ones
        if (firstImpact)
        {
            speed += 1f; // First impact increases the speed by 1
            firstImpact = false; // Mark the first impact as done
        }
        else
        {
            speed += 0.2f; // All subsequent impacts increase the speed by 0.2
        }

        // Set the new velocity in the opposite direction and apply the increased speed
        rb.velocity = newDirection * speed;

        Debug.Log("Collided! New speed: " + speed);
    }

    // Detects collision with a trigger, specifically the power-up
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has collided with a PowerUp
        if (other.CompareTag("PowerUp"))
        {
            // Apply the power-up effect: increase speed by 1.5x
            ApplyPowerUp();

            // Optionally destroy or disable the power-up object after collection
            Destroy(other.gameObject);

            Debug.Log("Power-up collected! Speed increased to: " + speed);
        }
    }

    // Applies the power-up effect to increase the speed
    void ApplyPowerUp()
    {
        speed *= powerUpMultiplier; // Increase the speed by 1.5x
        rb.velocity = rb.velocity.normalized * speed; // Adjust the velocity with the new speed
    }

    // Resets the object's position, speed, and state back to the initial values
    void ResetObjectState()
    {
        // Reduce HP by 1 via the HP UI Manager
        hpUIManager.DecreaseHP();

        // Set the initial position of the object
        transform.position = initialPosition;

        // Reset the speed to the initial value
        speed = 2f;

        // Reset the first impact flag
        firstImpact = true;

        // Initially set velocity to zero to keep the ball stationary
        rb.velocity = Vector2.zero;

        // Start the coroutine to delay applying the initial velocity
        StartCoroutine(ApplyInitialVelocityAfterDelay(2f)); // 2 seconds delay
    }

    void StartState()
    {
        // Set the initial position of the object
        transform.position = initialPosition;

        // Reset the speed to the initial value
        speed = 2f;

        // Reset the first impact flag
        firstImpact = true;

        // Initially set velocity to zero to keep the ball stationary
        rb.velocity = Vector2.zero;

        // Start the coroutine to delay applying the initial velocity
        StartCoroutine(ApplyInitialVelocityAfterDelay(2f)); // 2 seconds delay
    }
}
