using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StationController : MonoBehaviour
{
    public float rotateSpeed = 150f;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    public Slider healthSlider;
    private float currentHealth;

    [Header("Combat Settings")]
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 15f;    
    public float fireRate = 0.5f; 
    private float nextFireTime = 0f; 

    private HashSet<int> processedIDs = new HashSet<int>();

    void Awake() { gameObject.tag = "Station"; }

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    void Update()
{
    // Rotation logic...
    if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

    // --- IRON-CLAD SEMI-AUTO ---
    // 1. Check if the cooldown has finished first
    if (Time.time >= nextFireTime)
    {
        // 2. Only if the cooldown is over, do we listen for the key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            // 3. Lock the gun for the duration of fireRate
            nextFireTime = Time.time + fireRate;
        }
    }
}

    void LateUpdate() { processedIDs.Clear(); }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = -firePoint.up * bulletSpeed; 
        }
        Destroy(bullet, 3f);
    }

    public void TakeDamage(float amount, GameObject source)
    {
        if (source == null) return;

        int id = source.GetInstanceID();
        if (processedIDs.Contains(id)) return;
        processedIDs.Add(id);

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthSlider != null) healthSlider.value = currentHealth;

        Debug.Log($"Clean Hit! Source: {source.tag} | HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            UIManager.Instance.ShowGameOver();
        }

        // FIX: The object now disappears on hit
        Destroy(source); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ShipGreen") || collision.gameObject.CompareTag("ShipBlue"))
            TakeDamage(10f, collision.gameObject);
        else if (collision.gameObject.CompareTag("Asteroid"))
            TakeDamage(30f, collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            EnemyBullet bullet = other.GetComponent<EnemyBullet>();
            if (bullet != null && bullet.TryDealDamage())
                TakeDamage(5f, other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            TakeDamage(5f, other.gameObject);
        }
    }
}