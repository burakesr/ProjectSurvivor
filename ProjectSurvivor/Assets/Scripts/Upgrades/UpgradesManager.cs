using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeDataSO> upgrades;
    [SerializeField] private List<UpgradeDataSO> acquairedUpgrades = new List<UpgradeDataSO>();

    public List<UpgradeDataSO> selectedUpgrades = new List<UpgradeDataSO>();

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.GetLevelManager.levelData.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        player.GetLevelManager.levelData.OnLevelUp -= OnLevelUp;
    }

    public void Upgrade(int upgradeID)
    {
        UpgradeDataSO upgradeData = selectedUpgrades[upgradeID];

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                upgradeData.Upgrade();
                AddUpgradeToAvailabeUpgradesList(upgradeData.weaponData.UpgradeByLevel(upgradeData.weaponData.WeaponInstance.GetCurrentLevel));
                break;
            case UpgradeType.ItemUpgrade:
                upgradeData.UpgradeStat();
                break;
            case UpgradeType.WeaponUnlock:
                player.GetWeaponsManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                break;

            default:
                break;
        }

        acquairedUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public List<UpgradeDataSO> GetUpgrades(int count)
    {
        selectedUpgrades = new List<UpgradeDataSO>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        int x = 0;
        UpgradeDataSO[] removedUpgrades = new UpgradeDataSO[count];

        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, upgrades.Count);

            UpgradeDataSO selectedUpgrade = upgrades[random];

            if (selectedUpgrades.Contains(selectedUpgrade))
            {
                // Remove selected upgrade from upgrades list
                upgrades.Remove(selectedUpgrade);

                // Select random number without selected upgrade in the upgrades list
                random = Random.Range(0, upgrades.Count);
                selectedUpgrade = upgrades[random];

                // Add selected upgrade to upgradeList
                selectedUpgrades.Add(selectedUpgrade);

                removedUpgrades[x] = selectedUpgrade;
                x++;
            }
            else
            {
                selectedUpgrades.Add(selectedUpgrade);
                removedUpgrades[x] = selectedUpgrade;
                upgrades.Remove(selectedUpgrade);
                x++;
            }
        }

        upgrades.AddRange(removedUpgrades);

        return selectedUpgrades;
    }

    public void AddUpgradeToAvailabeUpgradesList(UpgradeDataSO upgrade)
    {
        upgrades.Add(upgrade);
    }

    private void OnLevelUp()
    {
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));
    }
}
