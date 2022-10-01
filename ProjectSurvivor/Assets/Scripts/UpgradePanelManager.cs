using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject panel;
    [SerializeField]
    private GameObject container;
    [SerializeField] 
    private UpgradeSelectionSlot selectionSlotPrefab;

    private List<UpgradeDataSO> m_upgrades = new List<UpgradeDataSO>();
    private List<UpgradeSelectionSlot> m_selectionSlots;

    private void Start()
    {
        GameManager.Instance.GetPlayer().GetLevelManager.levelData.OnLevelUp += OpenPanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.GetPlayer().GetLevelManager.levelData.OnLevelUp -= OpenPanel;
    }

    public void OpenPanel()
    {
        GameManager.Instance.PauseGame();

        m_upgrades = GameManager.Instance.GetPlayer().GetUpgradesManager.selectedUpgrades;

        CreateSelectionSlots(m_upgrades.Count);

        panel.SetActive(true);
    }

    private void CreateSelectionSlots(int slotCount)
    {
        m_selectionSlots = new List<UpgradeSelectionSlot>();

        for (int i = 0; i < slotCount; i++)
        {
            UpgradeSelectionSlot selectionSlot = Instantiate(selectionSlotPrefab, container.transform);
            selectionSlot.ButtonID = i;
            m_selectionSlots.Add(selectionSlot);
            m_selectionSlots[i].SetIcon(m_upgrades[i].upgradeIcon);
            m_selectionSlots[i].SetUpgradeNameText(m_upgrades[i].upgradeName);
            //m_selectionSlots[i].SetUpgradeLevelText(m_upgrades[i].weaponData.WeaponInstance.GetCurrentLevel.ToString());
        }
    }

    private void ClearSelectionSlots()
    {
        for (int i = 0; i < m_selectionSlots.Count; i++)
        {
            m_selectionSlots[i].Clear();
            Destroy(m_selectionSlots[i].gameObject);
            m_selectionSlots.RemoveAt(i);
            
            i--;
        }
    }

    public void ClosePanel()
    {
        GameManager.Instance.ResumeGame();

        ClearSelectionSlots();

        panel.SetActive(false);
    }
}
