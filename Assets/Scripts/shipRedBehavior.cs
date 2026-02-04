using UnityEngine;

public class shipRedBehavior : MonoBehaviour
{
    public Transform portalTransform;
    public float speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Always fly toward the center (Station)
        Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
        if (rb != null) rb.linearVelocity = direction * speed;

        // 2. CHECK FOR PORTAL (The Reward)
        // If it gets close to the Portal, give credits and score
        if (portalTransform != null && portalTransform.gameObject.activeSelf)
        {
            if (Vector2.Distance(transform.position, portalTransform.position) < 1.2f)
            {
                GiveReward();
            }
        }

        // 3. CHECK FOR STATION (The Penalty)
        // If it reaches the center (0,0) without hitting the portal first
        if (Vector2.Distance(transform.position, Vector2.zero) < 0.8f)
        {
            TakePenalty();
        }
    }

    void GiveReward()
    {
        if (UIManager.Instance != null) UIManager.Instance.AddCredits(30);
        if (GameManager.Instance != null) GameManager.Instance.AddReward(20, 0);
        
        Debug.Log("shipRed entered Portal! +30 Credits, +20 Score");
        Destroy(gameObject);
    }

    void TakePenalty()
    {
        if (GameManager.Instance != null)
        {
            // Subtract 10 from score (AddReward with a negative number)
            GameManager.Instance.AddReward(-10, 0);
        }
        
        Debug.Log("shipRed hit Station! -10 Score penalty");
        Destroy(gameObject);
    }
}