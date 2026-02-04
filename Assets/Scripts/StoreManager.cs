using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [Header("Station Parts")]
    public StationController station;
    public GameObject redPortal;
    public GameObject shieldSide;

    [Header("UI Buttons")]
    public Button weaponButton;
    public Button redPortalButton;
    public Button shieldButton;

    [Header("UI Text (Button Labels)")]
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI redPortalText;
    public TextMeshProUGUI shieldText;

    [Header("Costs")]
    public int weaponUpgradeCost = 150;
    public int redPortalCost = 500;
    public int shieldCost = 300;

    void Start()
    {
        // When the game starts, check what we already own
        LoadUpgrades();
    }

    void LoadUpgrades()
    {
        // Load Red Portal (1 = Owned, 0 = Not Owned)
        if (PlayerPrefs.GetInt("SavedRedPortal", 0) == 1)
        {
            if (redPortal != null) redPortal.SetActive(true);
            redPortalButton.interactable = false;
            redPortalText.text = "UNLOCKED";
        }

        // Load Shield
        if (PlayerPrefs.GetInt("SavedShield", 0) == 1)
        {
            if (shieldSide != null) shieldSide.SetActive(true);
            shieldButton.interactable = false;
            shieldText.text = "MOUNTED";
        }

        // Load Weapon Upgrade
        if (PlayerPrefs.GetInt("SavedWeapon", 0) == 1)
        {
            if (station != null) station.fireRate -= 0.15f;
            weaponButton.interactable = false;
            weaponText.text = "MAXED";
        }
    }

    public void BuyWeaponUpgrade()
    {
        if (station != null && UIManager.Instance.SpendCredits(weaponUpgradeCost))
        {
            station.fireRate -= 0.15f;
            weaponButton.interactable = false;
            weaponText.text = "MAXED";
            
            // AUTO-SAVE
            PlayerPrefs.SetInt("SavedWeapon", 1);
            PlayerPrefs.Save();
            Debug.Log("Weapon Upgrade Saved!");
        }
    }

    public void BuyRedPortal()
    {
        if (redPortal != null && !redPortal.activeSelf)
        {
            if (UIManager.Instance.SpendCredits(redPortalCost))
            {
                redPortal.SetActive(true);
                redPortalButton.interactable = false;
                redPortalText.text = "UNLOCKED";
                
                // AUTO-SAVE
                PlayerPrefs.SetInt("SavedRedPortal", 1);
                PlayerPrefs.Save();
                Debug.Log("Red Portal Saved!");
            }
        }
    }

    public void BuyShieldSide()
    {
        if (shieldSide != null && !shieldSide.activeSelf)
        {
            if (UIManager.Instance.SpendCredits(shieldCost))
            {
                shieldSide.SetActive(true);
                shieldButton.interactable = false;
                shieldText.text = "MOUNTED";
                
                // AUTO-SAVE
                PlayerPrefs.SetInt("SavedShield", 1);
                PlayerPrefs.Save();
                Debug.Log("Shield Saved!");
            }
        }
    }
}