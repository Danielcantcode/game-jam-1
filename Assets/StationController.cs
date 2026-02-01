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
    public float bulletSpeed = 15f;

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
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

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

    // --- NEW: HULL COLLISION LOGIC ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If a ship hits the station body (NOT the portals)
        if (collision.gameObject.CompareTag("ShipGreen") || 
            collision.gameObject.CompareTag("ShipBlue") || 
            collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10f); // Lose 10 HP
            Destroy(collision.gameObject); // Remove ship
            Debug.Log("Hull hit by " + collision.gameObject.tag + "! -10 HP");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
            // We can add a proper Game Over screen call here later!
        }
    }
}