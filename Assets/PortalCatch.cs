using UnityEngine;

public class PortalCatch : MonoBehaviour
{
    public string myColor; // Type "Blue" or "Green" in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. CORRECT CATCH
        if (myColor == "Blue" && other.CompareTag("ShipBlue"))
        {
            GameManager.Instance.AddReward(20, 25);
            Destroy(other.gameObject);
        }
        else if (myColor == "Green" && other.CompareTag("ShipGreen"))
        {
            GameManager.Instance.AddReward(10, 15);
            Destroy(other.gameObject);
        }

        // 2. ENEMY RAID (Enemy hits a portal)
        else if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.LoseCredits(10); 
            
            StationController station = GetComponentInParent<StationController>();
            if (station != null)
            {
                station.TakeDamage(5f);
            }
            
            Destroy(other.gameObject);
        }

        // 3. WRONG COLOR PENALTIES
        // If a Blue ship hits the Green portal
        else if (other.CompareTag("ShipBlue"))
        {
            GameManager.Instance.AddReward(-15, 0); // Blue penalty: -15
            Destroy(other.gameObject);
        }
        // If a Green ship hits the Blue portal
        else if (other.CompareTag("ShipGreen"))
        {
            GameManager.Instance.AddReward(-10, 0); // Green penalty: -10
            Destroy(other.gameObject);
        }
    }
}