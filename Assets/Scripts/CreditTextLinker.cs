using UnityEngine;
using TMPro;

public class CreditTextLinker : MonoBehaviour
{
    public bool isMainMenuText;

    void Start() 
{
    // Wait a tiny fraction of a second for GameManager to Load PlayerPrefs
    Invoke("LinkToUI", 0.1f);
}

void LinkToUI()
{
    if (UIManager.Instance != null)
    {
        UIManager.Instance.RegisterCreditsText(GetComponent<TextMeshProUGUI>(), isMainMenuText);
    }
}
}