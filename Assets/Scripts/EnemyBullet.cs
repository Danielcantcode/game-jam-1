using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15f;
    private bool hasDealtDamage = false; // The Lock

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        Destroy(gameObject, 5f);
    }

    // This function returns TRUE only the first time it is called
    public bool TryDealDamage()
    {
        if (hasDealtDamage) return false;
        hasDealtDamage = true;
        return true;
    }
}