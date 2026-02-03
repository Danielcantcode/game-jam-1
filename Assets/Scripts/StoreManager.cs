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

    // 1. WEAPON (One-time Upgrade)
    public void BuyWeaponUpgrade()
    {
        if (station != null && GameManager.Instance.SpendCredits(weaponUpgradeCost))
        {
            station.fireRate -= 0.15f; // Significant one-time boost
            
            weaponButton.interactable = false;
            weaponText.text = "MAXED";
            
            Debug.Log("Weapon Permanently Upgraded!");
        }
    }

    // 2. RED PORTAL
    public void BuyRedPortal()
    {
        if (redPortal != null && !redPortal.activeSelf)
        {
            if (GameManager.Instance.SpendCredits(redPortalCost))
            {
                redPortal.SetActive(true);
                
                redPortalButton.interactable = false;
                redPortalText.text = "UNLOCKED";
                
                Debug.Log("Red Portal Unlocked!");
            }
        }
    }

    // 3. SHIELD SIDE
    public void BuyShieldSide()
    {
        if (shieldSide != null && !shieldSide.activeSelf)
        {
            if (GameManager.Instance.SpendCredits(shieldCost))
            {
                shieldSide.SetActive(true);
                
                shieldButton.interactable = false;
                shieldText.text = "MOUNTED";
                
                Debug.Log("Shield Side Mounted!");
            }
        }
    }
}