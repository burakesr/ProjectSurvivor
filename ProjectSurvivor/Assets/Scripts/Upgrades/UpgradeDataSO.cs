using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData_New", menuName = "ScriptableObjects/Upgrade/Upgrade Data")]
public class UpgradeDataSO : ScriptableObject
{
    [Header("GENERAL INFO")]
    public UpgradeType upgradeType;
    public string upgradeName;
    public Sprite upgradeIcon;

    [Space(10)]
    [Header("WEAPON UPGRADE INFO")]
    public WeaponDataSO weaponData;
    public WeaponUpgradeSO weaponUpgrade;

    [Space(10)]
    [Header("STAT UPGRADE INFO")]
    public StatConfigureSO statConfig;
    public int upgradeValue;


    public void Upgrade()
    {
        if (weaponUpgrade)
        {
            weaponData.WeaponInstance.LevelUp();
            weaponUpgrade.Upgrade(weaponData);
        }
    }

    public void UpgradeStat()
    {
        StatsManager statManager = GameManager.Instance.GetPlayer().GetStatsManager;

        switch (statConfig.statType)
        {
            case StatTypes.Armor:
                statManager.GetArmorStat.Upgrade(upgradeValue);
                break;
            case StatTypes.MaxHealth:
                statManager.GetMaxHealthStat.Upgrade(upgradeValue);
                break;
            case StatTypes.Recovery:
                statManager.GetRecoveryStat.Upgrade(upgradeValue);
                break;
            case StatTypes.Strength:
                statManager.GetStrengthStat.Upgrade(upgradeValue);
                break;

            default:
                break;
        }
    }
}