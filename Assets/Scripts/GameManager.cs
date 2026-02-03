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
    public TextMeshProUGUI hudCreditText;   // Rename this to be specific
    public TextMeshProUGUI menuCreditText;  // NEW: Add this for the Main Menu
    public TextMeshProUGUI storeCreditText; // NEW: Add this for the Store Panel

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 1. LOAD: Pull the saved credits from the device memory
        credits = PlayerPrefs.GetInt("TotalCredits", 0); 
        UpdateUI();
    }

    public void AddReward(int scoreAmount, int creditAmount)
    {
        score += scoreAmount;
        credits += creditAmount;

        if (score < 0) score = 0;
        if (credits < 0) credits = 0;

        SaveCredits();
        UpdateUI();
    }

    public void LoseCredits(int amount)
    {
        credits -= amount;
        if (credits < 0) credits = 0;

        SaveCredits();
        UpdateUI();
    }

    public bool SpendCredits(int cost)
    {
        if (credits >= cost)
        {
            credits -= cost;
            SaveCredits();
            UpdateUI();
            return true; 
        }
        
        Debug.Log("Not enough credits!");
        return false; 
    }

    // Helper to keep the code clean
    private void SaveCredits()
    {
        PlayerPrefs.SetInt("TotalCredits", credits);
        PlayerPrefs.Save();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "SCORE\n" + score;
        
        // Update the HUD display
        if (hudCreditText != null) hudCreditText.text = "CREDITS\n" + credits;
        
        // Update the Main Menu display
        if (menuCreditText != null) menuCreditText.text = "CREDITS\n " + credits;
        
        // NEW: Update the Store
    if (storeCreditText != null) storeCreditText.text = "WALLET: " + credits;
    }
}