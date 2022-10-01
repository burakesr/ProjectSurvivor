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
    public AbilityDataSO abilityData;
    public AbilityUpgradeSO abilityUpgrade;

    [Space(10)]
    [Header("STAT UPGRADE INFO")]
    public StatConfigureSO statConfig;
    public int upgradeValue;


    public void Upgrade()
    {
        if (abilityUpgrade)
        {
            abilityData.AbilityInstance.LevelUp();
            abilityUpgrade.Upgrade(abilityData);
        }
    }

    public void UpgradeStat()
    {
        switch (statConfig.statType)
        {
            case StatTypes.Armor:
                StatsManager.Instance.GetArmorStat.Upgrade(upgradeValue);
                break;
            case StatTypes.MaxHealth:
                StatsManager.Instance.GetMaxHealthStat.Upgrade(upgradeValue);
                break;
            case StatTypes.Recovery:
                StatsManager.Instance.GetRecoveryStat.Upgrade(upgradeValue);
                break;
            case StatTypes.Strength:
                StatsManager.Instance.GetStrengthStat.Upgrade(upgradeValue);
                break;

            default:
                break;
        }
    }
}