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
    public float spawnDistance = 15f; 

    void Start()
    {
        InvokeRepeating("SpawnRandomShip", 2f, spawnRate);
    }

    void SpawnRandomShip()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnDistance;
        int choice = Random.Range(0, 4); 
        GameObject prefabToSpawn = null;

        // Clean selection logic
        if (choice == 0) prefabToSpawn = shipGreenPrefab;
        else if (choice == 1) prefabToSpawn = shipBluePrefab;
        else if (choice == 2) prefabToSpawn = enemyPrefab;
        else prefabToSpawn = asteroidPrefab; 

        if (prefabToSpawn != null)
        {
            GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (Vector2.zero - spawnPos).normalized;
                // Note: Ensure your objects have 'Gravity Scale' set to 0 in Rigidbody2D!
                rb.linearVelocity = direction * 3f; 
            }
        }
    }
}