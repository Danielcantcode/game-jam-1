using UnityEngine;

public class PortalCatch : MonoBehaviour
{
    [Tooltip("Set this to 'Blue' or 'Green' in the Inspector")]
    public string myColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. IGNORE BULLETS
        if (other.CompareTag("EnemyBullet")) return;

        // 2. CORRECT CATCH (Reward: Score & Credits)
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

        // 3. ENEMY RAID (Penalty: Credits AND 5 HP)
        else if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.LoseCredits(10);
            
            StationController station = GetComponentInParent<StationController>();
            if (station != null) 
            {
                // We pass the object so the Station knows to lock it from double-damage
                station.TakeDamage(5f, other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        // 4. WRONG COLOR PENALTIES
        else if (myColor == "Green" && other.CompareTag("ShipBlue"))
        {
            GameManager.Instance.AddReward(-15, 0); 
            Destroy(other.gameObject);
        }
        else if (myColor == "Blue" && other.CompareTag("ShipGreen"))
        {
            GameManager.Instance.AddReward(-10, 0); 
            Destroy(other.gameObject);
        }
    }
}