using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    public float speed = 1f; 
    public Vector2 initialPosition = new Vector2(0, 0); 
    private Rigidbody2D rb;
    private Vector2 lastVelocity; 
    private bool firstImpact = true; 

    
    public HpManager hpUIManager;

    
    public float powerUpMultiplier = 1.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartState(); 
    }

    void Update()
    {
        
        lastVelocity = rb.velocity;

        
        if (transform.position.y <= -5f)
        {
            Debug.Log("Object went out of bounds! Resetting...");
            ResetObjectState(); 
        }
    }

    
    IEnumerator ApplyInitialVelocityAfterDelay(float delay)
    {
        
        yield return new WaitForSeconds(delay);

        
        rb.velocity = new Vector2(speed, -0.4f);

        Debug.Log("Initial velocity applied: " + rb.velocity);
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Vector2 newDirection = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        
        if (firstImpact)
        {
            speed += 1f; 
            firstImpact = false; 
        }
        else
        {
            speed += 0.2f; 
        }

        
        rb.velocity = newDirection * speed;

        Debug.Log("Collided! New speed: " + speed);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PowerUp"))
        {
            
            ApplyPowerUp();

            
            Destroy(other.gameObject);

            Debug.Log("Power-up collected! Speed increased to: " + speed);
        }
    }

    
    void ApplyPowerUp()
    {
        speed *= powerUpMultiplier; 
        rb.velocity = rb.velocity.normalized * speed; 
    }

   
    void ResetObjectState()
    {
        hpUIManager.DecreaseHP();
        
        transform.position = initialPosition;
        
        speed = 2f;
        
        firstImpact = true;
        
        rb.velocity = Vector2.zero;
        
        StartCoroutine(ApplyInitialVelocityAfterDelay(2f));
    }

    void StartState()
    {
        transform.position = initialPosition;

        speed = 2f;

        firstImpact = true;

        rb.velocity = Vector2.zero;

        StartCoroutine(ApplyInitialVelocityAfterDelay(2f)); 
    }
}
