using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject tutorialPanel; // Drag "HowToPlayPanel" here
    public GameObject storePanel;

    void Awake()
    {
        // Singleton setup to allow other scripts to call UIManager.Instance
        if (Instance == null) Instance = this;
    }

    // --- GAMEPLAY LOGIC ---

    public void StartGame()
    {
        Time.timeScale = 1f; // Ensure time is moving
        // Replace "GameScene" with your actual game level name
        SceneManager.LoadScene("GameScene"); 
    }

    public void RestartGame()
    {
        Debug.Log("Restart Button Triggered!"); 
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Freeze the game
        }
    }

    // --- NAVIGATION LOGIC ---

    public void OpenOptions() { SwitchPanel(optionsPanel); }
    public void OpenTutorial() { SwitchPanel(tutorialPanel); }
    public void OpenStore() { SwitchPanel(storePanel); }
    public void BackToMain() { SwitchPanel(mainPanel); }

    // This helper function hides everything and shows the one you clicked
    private void SwitchPanel(GameObject targetPanel)
    {
        // Disable all panels first
        if (mainPanel != null) mainPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        if (storePanel != null) storePanel.SetActive(false);

        // Enable the specific one we want
        if (targetPanel != null) targetPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}