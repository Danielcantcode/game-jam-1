using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject shipGreenPrefab;
    public GameObject shipBluePrefab;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject shipRedPrefab; 

    [Header("Settings")]
    public float spawnRate = 2f;
    public float spawnDistance = 15f; 

    [Header("Upgrade Reference")]
    public GameObject redPortalObject;

    void Start()
    {
        InvokeRepeating("SpawnRandomShip", 2f, spawnRate);
    }

    void SpawnRandomShip()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnDistance;
        
        int maxChoice = (redPortalObject != null && redPortalObject.activeSelf) ? 5 : 4;
        int choice = Random.Range(0, maxChoice); 
        
        GameObject prefabToSpawn = null;

        if (choice == 0) prefabToSpawn = shipGreenPrefab;
        else if (choice == 1) prefabToSpawn = shipBluePrefab;
        else if (choice == 2) prefabToSpawn = enemyPrefab;
        else if (choice == 3) prefabToSpawn = asteroidPrefab;
        else prefabToSpawn = shipRedPrefab; 

        if (prefabToSpawn != null)
    {
        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - spawnPos).normalized;
            rb.linearVelocity = direction * 2f; 
        }

        // This part MUST match the variables in shipRedBehavior
        if (prefabToSpawn == shipRedPrefab)
        {
            shipRedBehavior shipScript = newObject.AddComponent<shipRedBehavior>();
            
            // Check if we have a portal reference to give to the ship
            if (redPortalObject != null)
            {
                shipScript.portalTransform = redPortalObject.transform;
            }
        }
        }
    }
}