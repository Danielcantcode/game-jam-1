using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Slightly faster for better feel
    public float lifeTime = 3f;
    private bool hasHit = false;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Use transform.up if your bullet's "tip" is the local Y axis
           rb.linearVelocity = -transform.up * speed;
        }
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        // 1. HIT ENEMIES
        if (other.CompareTag("Enemy"))
        {
            hasHit = true;
            GameManager.Instance.AddReward(5, 5);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // 2. HIT ALLIES (Penalty)
        else if (other.CompareTag("ShipGreen") || other.CompareTag("ShipBlue"))
        {
            hasHit = true;
            GameManager.Instance.AddReward(-10, 0); 
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // 3. HIT ASTEROIDS
        else if (other.CompareTag("Asteroid"))
        {
            hasHit = true;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}