using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject hudPanel;
    public GameObject pausePanel;
    public GameObject storePanel;
    public GameObject optionsPanel;
    public GameObject tutorialPanel;
    public GameObject gameOverPanel;

    [Header("Credit Displays")]
    public TextMeshProUGUI mainMenuCreditText;
    public TextMeshProUGUI storeCreditText;
    
    // static ensures this persists in memory as long as the game is running
    private static int currentCredits = 0; 
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            // Uncomment this to keep one manager alive forever
            // DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 0f;
    }

   void Start()
    {
        // Much cleaner! Just set the initial menu state.
        ShowInitialMenu();
        UpdateCreditsDisplay(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainPanel.activeSelf && !gameOverPanel.activeSelf)
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }
    }

    // --- ECONOMY ---

    public void AddCredits(int amount)
    {
        currentCredits += amount;
        UpdateCreditsDisplay();
    }


    // Add this so the text objects can "find" the manager themselves
    public void RegisterCreditsText(TextMeshProUGUI textObj, bool isMainMenu)
    {
        if (isMainMenu) mainMenuCreditText = textObj;
        else storeCreditText = textObj;
        
        UpdateCreditsDisplay(); // Immediately show the right number
    }

    public void UpdateCreditsDisplay()
    {
        string textValue = "Credits\n " + currentCredits.ToString();
        
        if (mainMenuCreditText != null) mainMenuCreditText.text = textValue;
        if (storeCreditText != null) storeCreditText.text = textValue;
    }

    // --- GAMEPLAY FLOW ---

    public void StartGame()
    {
        isPaused = false; // Ensure this is false when game starts
        if (mainPanel != null) mainPanel.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(true);
        Time.timeScale = 1f; 
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
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
        // Set time back to normal so the scene can load, 
        // but the new scene's Awake will freeze it again
        Time.timeScale = 1f; 
        isPaused = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- NAVIGATION ---

    public void OpenOptions() { SwitchPanel(optionsPanel); }
    public void OpenTutorial() { SwitchPanel(tutorialPanel); }
    
    public void OpenStore() 
    { 
        UpdateCreditsDisplay(); 
        SwitchPanel(storePanel); 
    }

    public void BackToMain() 
    { 
        UpdateCreditsDisplay(); 
        
        if (isPaused) 
        {
            SwitchPanel(pausePanel);
        }
        else 
        {
            SwitchPanel(mainPanel); 
        }
    }

    private void ShowInitialMenu()
    {
        if (hudPanel != null) hudPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    private void SwitchPanel(GameObject targetPanel)
    {
        if (mainPanel) mainPanel.SetActive(false);
        if (optionsPanel) optionsPanel.SetActive(false);
        if (tutorialPanel) tutorialPanel.SetActive(false);
        if (storePanel) storePanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);

        if (targetPanel != null) targetPanel.SetActive(true);
    }
}