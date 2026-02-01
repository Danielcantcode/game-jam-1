using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float fireRate = 2.0f;
    private float nextFireTime;

    void Start()
    {
        // Randomize the first shot so they don't all fire at the exact same time
        nextFireTime = Time.time + Random.Range(0f, fireRate);
    }

    void Update()
{
    float distanceToCenter = Vector2.Distance(transform.position, Vector2.zero);

    // INCREASED: Now they shoot from further away (13 units instead of 9)
    if (distanceToCenter < 13f) 
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
}
    void Shoot()
{
    if (enemyBulletPrefab != null)
    {
        // Calculate direction toward center (0,0)
        Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;

        // Offset the spawn position by 1 unit in the direction of the center
        // This makes the bullet appear at the "nose" of the ship
        Vector3 spawnPos = transform.position + (Vector3)(direction * 2.0f);

        Instantiate(enemyBulletPrefab, spawnPos, Quaternion.identity);
    }
}
}