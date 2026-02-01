using UnityEngine;
using TMPro; // Use this if you are using TextMeshPro for UI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // This lets other scripts find the manager easily

    public int score = 0;
    public int credits = 0;

    public TextMeshProUGUI scoreText;   // Drag your UI text here
    public TextMeshProUGUI creditText;  // Drag your UI text here

    void Awake()
    {
        Instance = this; // Set up the "shortcut" to this script
    }

   public void AddReward(int scoreAmount, int creditAmount)
    {
    score += scoreAmount;
    credits += creditAmount;

    // --- THE FIX ---
    // If score goes below 0, reset it to 0
    if (score < 0) 
    {
        score = 0;
    }

    // You can do the same for credits if you want!
    if (credits < 0)
    {
        credits = 0;
    }

    UpdateUI();
    }
public void LoseCredits(int amount)
    {
    credits -= amount;

    // Prevent credits from going below zero
    if (credits < 0)
    {
        credits = 0;
    }

    UpdateUI(); // Make sure the screen updates with the new number
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (creditText != null) creditText.text = "Credits: " + credits;
    }
}