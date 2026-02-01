using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float fireRate = 5.0f; // Seconds between shots
    private float nextFireTime;

    void Start()
    {
        // Randomize the first shot so they don't all fire at once
        nextFireTime = Time.time + Random.Range(0f, fireRate);
    }

    void Update()
    {
        float distanceToCenter = Vector2.Distance(transform.position, Vector2.zero);

        // Only shoot when within range
        if (distanceToCenter < 9f) 
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                // SET THE COOLDOWN: This is the "control"
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        // Check if prefab exists and only call Instantiate ONCE
        if (enemyBulletPrefab != null)
        {
            Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

            // Spawn exactly one bullet
            Instantiate(enemyBulletPrefab, transform.position, bulletRotation);
        }
    }
}