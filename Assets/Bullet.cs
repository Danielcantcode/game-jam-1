using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            // Pushes the bullet forward. 
            // If it goes the wrong way, change -transform.up to transform.up
            rb.linearVelocity = -transform.up * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        GameManager.Instance.AddReward(5, 5);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    else if (other.CompareTag("ShipGreen") || other.CompareTag("ShipBlue"))
    {
        GameManager.Instance.AddReward(-10, 0); 
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    // --- BLOW UP ASTEROIDS ---
    else if (other.CompareTag("Asteroid"))
    {
        // No points rewarded, just destroy both
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
}