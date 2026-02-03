using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject hudPanel;      // Drag your Health/Score UI here
    public GameObject pausePanel;    // Create a new Panel for Pause
    public GameObject storePanel;
    public GameObject optionsPanel;
    public GameObject tutorialPanel;
    public GameObject gameOverPanel;

    private bool isPaused = false;

    void Awake()
    {
        // Singleton setup with a "Duplicate Guard"
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        // Start the game frozen so the player can use the Main Menu
        Time.timeScale = 0f;
    }

    void Start()
    {
        // Set initial state: Only Main Menu visible
        ShowInitialMenu();
    }

    void Update()
    {
        // ESC Key logic for Pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Only allow pausing if we aren't in the Main Menu or Game Over
            if (!mainPanel.activeSelf && !gameOverPanel.activeSelf)
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }
    }

    // --- GAMEPLAY FLOW ---

    public void StartGame()
    {
        mainPanel.SetActive(false);
        hudPanel.SetActive(true);
        Time.timeScale = 1f; // Unfreeze the world
        Debug.Log("Game Started!");
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Freeze logic
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Back to action
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        hudPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Exiting...");
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void QuitToMainMenuFromPause()
{
    // 1. Unfreeze time so the scene can load properly
    Time.timeScale = 1f; 

    // 2. Reload the current scene
    // Because your UIManager script has 'Time.timeScale = 0f' in Awake,
    // it will automatically start at the Main Menu when it reloads.
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    // --- NAVIGATION ---

    public void OpenOptions() { SwitchPanel(optionsPanel); }
    public void OpenTutorial() { SwitchPanel(tutorialPanel); }
    public void OpenStore() { SwitchPanel(storePanel); }
    public void BackToMain() { SwitchPanel(mainPanel); }

    private void ShowInitialMenu()
    {
        if (hudPanel != null) hudPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    private void SwitchPanel(GameObject targetPanel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        storePanel.SetActive(false);

        if (targetPanel != null) targetPanel.SetActive(true);
    }
}