using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [Header("Ship Prefabs")]
    public GameObject shipGreenPrefab;
    public GameObject shipBluePrefab;
    public GameObject enemyPrefab;

    [Header("Settings")]
    public float spawnRate = 2f;
    public float spawnDistance = 15f; // How far off-screen they start

    void Start()
    {
        // Start spawning after 2 seconds, then every 'spawnRate' seconds
        InvokeRepeating("SpawnRandomShip", 2f, spawnRate);
    }

    void SpawnRandomShip()
    {
        // 1. Pick a random position on a big circle around the station
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnDistance;

        // 2. Randomly choose which type of ship to spawn
        int choice = Random.Range(0, 3); // Picks 0, 1, or 2
        GameObject prefabToSpawn;

        if (choice == 0) prefabToSpawn = shipGreenPrefab;
        else if (choice == 1) prefabToSpawn = shipBluePrefab;
        else prefabToSpawn = enemyPrefab;

        // 3. Create the ship
        GameObject newShip = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // 4. Give it a push toward the center (0,0)
        Rigidbody2D rb = newShip.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - spawnPos).normalized;
            rb.linearVelocity = direction * 3f; // You can make this faster over time!
        }
    }
}