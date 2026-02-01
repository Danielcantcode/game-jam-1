using UnityEngine;
using UnityEngine.UI;

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
    public float fireRate = 0.3f;   
    private float nextFireTime = 0f; 

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
        // Rotation logic
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

        // Shooting logic
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    // This handles the physical "thud" of things hitting your hull
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Normal ships = 10 damage
        if (collision.gameObject.CompareTag("ShipGreen") || 
            collision.gameObject.CompareTag("ShipBlue") || 
            collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10f); 
            Destroy(collision.gameObject);
        }
        // Asteroids = 30 damage
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(30f); 
            Destroy(collision.gameObject);
        }
    }

    // This MUST be 'public' so the Bullet script can see it!
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Station hit! Current Health: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // GameManager.Instance.EndGame(); // Uncomment when GameManager is ready
        }
    }

private void OnTriggerEnter2D(Collider2D other)
{
    // Make sure this matches the Tag you just created!
    if (other.CompareTag("Station"))
    {
        // 1. Tell the station to take damage (This happens in StationController)
        // 2. Destroy this bullet immediately
        Destroy(gameObject);
    }
}
}