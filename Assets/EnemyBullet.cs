using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f; 

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        Destroy(gameObject, 5f);
    }

    // --- THIS IS THE MISSING PIECE ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit the Station
        // Ensure your Station object has the tag "Player" or "Station"
        if (other.CompareTag("Player") || other.CompareTag("Station"))
        {
            // The Station script handles the HP loss, 
            // the bullet just needs to "pop" (disappear) now.
            Destroy(gameObject);
        }
    }
}