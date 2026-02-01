using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;

    // This runs when the bullet's Trigger touches another Collider2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. HIT AN ENEMY (Good!)
        if (other.CompareTag("Enemy")) 
        {
            // Access the GameManager to add 5 Score
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddReward(5, 0); 
            }

            Destroy(other.gameObject); // Destroys the Enemy Ship
            Destroy(gameObject);      // Destroys the Bullet
        }

        // 2. HIT A GOOD SHIP (Bad!)
        else if (other.CompareTag("ShipGreen") || other.CompareTag("ShipBlue"))
        {
            // Access the GameManager to subtract 10 Score
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddReward(-10, 0); 
            }

            Destroy(other.gameObject); // Destroys the Green/Blue Ship
            Destroy(gameObject);      // Destroys the Bullet
        }
    }

    // Automatically destroys the bullet when it leaves the screen to save memory
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}