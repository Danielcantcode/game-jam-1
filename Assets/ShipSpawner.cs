using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject shipGreenPrefab;
    public GameObject shipBluePrefab;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;

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
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnDistance;

        // 1. Increase range to 4 (Picks 0, 1, 2, or 3)
        int choice = Random.Range(0, 4); 
        GameObject prefabToSpawn;

        // 2. Add the logic for the 4th option
        if (choice == 0) 
            prefabToSpawn = shipGreenPrefab;
        else if (choice == 1) 
            prefabToSpawn = shipBluePrefab;
        else if (choice == 2) 
            prefabToSpawn = enemyPrefab;
        else 
            prefabToSpawn = asteroidPrefab; // Now choice '3' spawns the asteroid!

        // 3. Create the object
        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // 4. Give it a push toward the center
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - spawnPos).normalized;
            rb.linearVelocity = direction * 3f; 
        }
    }
}