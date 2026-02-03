using UnityEngine;

public class ShieldSideLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // The shield only vaporizes things that hit this specific side of the station
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Asteroid") || other.CompareTag("Enemy"))
        {
            Debug.Log("Shield Side Blocked: " + other.tag);
            
            // Play a small effect here if you have one
            Destroy(other.gameObject); 
        }
    }
}