using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Economy Settings")]
    public int score = 0;
    public int credits = 0;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;   
    public TextMeshProUGUI creditText;  

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // Handles adding (or subtracting) rewards and scores
    public void AddReward(int scoreAmount, int creditAmount)
    {
        score += scoreAmount;
        credits += creditAmount;

        // Clamp values so they never go below zero
        if (score < 0) score = 0;
        if (credits < 0) credits = 0;

        UpdateUI();
    }

    // Specific function for penalties (like when enemies hit the portal)
    public void LoseCredits(int amount)
    {
        credits -= amount;
        if (credits < 0) credits = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "SCORE\n" + score;
        if (creditText != null) creditText.text = "CREDITS\n" + credits;
    }
}