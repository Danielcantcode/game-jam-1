using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private static bool shouldStartInGame = false;

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

    [Header("Feedback UI")]
    public GameObject warningMessage; 
    
    // REDIRECT: currentCredits now pulls directly from your GameManager data
    private int currentCredits 
    {
        get { return GameManager.Instance != null ? GameManager.Instance.credits : 0; }
    }

    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            // Persistence is handled by GameManager's PlayerPrefs, 
            // but we keep the Instance alive for UI logic.
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
        if (shouldStartInGame)
        {
            shouldStartInGame = false; 
            StartGame(); 
        }
        else
        {
            ShowInitialMenu();
            UpdateCreditsDisplay();
        }
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

    // --- ECONOMY (LINKED TO GAMEMANAGER) ---

    public void AddCredits(int amount)
    {
        // Redirect to GameManager so it saves via PlayerPrefs
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddReward(0, amount);
            UpdateCreditsDisplay();
        }
    }

    public void RegisterCreditsText(TextMeshProUGUI textObj, bool isMainMenu)
    {
        if (isMainMenu) mainMenuCreditText = textObj;
        else storeCreditText = textObj;
        
        UpdateCreditsDisplay();
    }

    public void UpdateCreditsDisplay()
    {
        // Pulling directly from the property that looks at GameManager.Instance.credits
        string textValue = "Credits\n " + currentCredits.ToString();
        
        if (mainMenuCreditText != null) mainMenuCreditText.text = textValue;
        if (storeCreditText != null) storeCreditText.text = textValue;
    }

    public bool SpendCredits(int amount)
    {
        // Redirect to GameManager's spend logic to ensure it saves correctly
        if (GameManager.Instance != null && GameManager.Instance.SpendCredits(amount))
        {
            UpdateCreditsDisplay();
            return true;
        }
        
        ShowWarning(); 
        return false;
    }

    private void ShowWarning()
    {
        if (warningMessage != null)
        {
            StopAllCoroutines(); 
            StartCoroutine(HideWarningAfterDelay(2f));
        }
    }

    private IEnumerator HideWarningAfterDelay(float delay)
    {
        warningMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(delay); 
        warningMessage.SetActive(false);
    }

    // --- GAMEPLAY FLOW ---

    public void StartGame()
    {
        isPaused = false; 
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
        shouldStartInGame = true; 
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
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void QuitToMainMenuFromPause()
    {
        Time.timeScale = 1f; 
        isPaused = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- NAVIGATION ---

    public void OpenOptions() { SwitchPanel(optionsPanel); }
    public void OpenTutorial() { SwitchPanel(tutorialPanel); }
    
    public void OpenStore() 
    { 
        if (warningMessage != null) warningMessage.SetActive(false); 
        UpdateCreditsDisplay(); 
        SwitchPanel(storePanel); 
    }

    public void BackToMain() 
    { 
        StopAllCoroutines();
        if (warningMessage != null) warningMessage.SetActive(false); 
        
        UpdateCreditsDisplay(); 
        
        if (isPaused) SwitchPanel(pausePanel);
        else SwitchPanel(mainPanel); 
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