using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 5f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Fly toward the center
            Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Search for the script on the object hit OR its parents
        StationController station = other.GetComponentInParent<StationController>();

        if (station != null)
        {
            station.TakeDamage(damage); // Call the function
            Destroy(gameObject);        // Destroy bullet on impact
        }
    }
}