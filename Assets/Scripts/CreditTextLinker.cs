using UnityEngine;
using TMPro;

public class CreditTextLinker : MonoBehaviour
{
    public bool isMainMenuText;

    void Start() // Start runs after Awake, ensuring Instance is set
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterCreditsText(GetComponent<TextMeshProUGUI>(), isMainMenuText);
        }
    }
    
    void OnEnable() // Keep this for when you switch panels without reloading
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterCreditsText(GetComponent<TextMeshProUGUI>(), isMainMenuText);
        }
    }
}