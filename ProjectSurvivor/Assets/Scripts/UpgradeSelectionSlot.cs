using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelectionSlot : MonoBehaviour
{
    [SerializeField] 
    private Image iconImage;
    [SerializeField] 
    private TextMeshProUGUI upgradeLevelText;
    [SerializeField] 
    private TextMeshProUGUI upgradeNameText;
    [SerializeField] 
    private TextMeshProUGUI upgradeDescriptionText;
    
    private UpgradePanelManager m_upgradePanelManager;

    public int ButtonID { get; set; }


    private void Awake()
    {
        m_upgradePanelManager = FindObjectOfType<UpgradePanelManager>();
    }

    public void SetIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }

    public void SetUpgradeLevelText(string upgradeLevel)
    {
        upgradeLevelText.text = upgradeLevel;
    }

    public void SetUpgradeNameText(string upgradeName)
    {
        upgradeNameText.text = upgradeName;
    }

    public void SetUpgradeDescriptionText(string upgradeDescription)
    {
        upgradeDescriptionText.text = upgradeDescription;
    }

    public void Clear()
    {
        SetIcon(null);
        SetUpgradeDescriptionText(null);
        SetUpgradeLevelText(null);
        SetUpgradeNameText(null);
    }

    public void ApplyUpgrade()
    {
        GameManager.Instance.GetPlayer().GetUpgradesManager.Upgrade(ButtonID);

        Debug.Log("Player pressed:" + ButtonID);
        m_upgradePanelManager.ClosePanel();
    }
}
