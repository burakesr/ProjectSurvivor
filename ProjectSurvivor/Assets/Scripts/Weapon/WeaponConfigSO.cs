using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig_New", menuName = "ScriptableObjects/Weapon/WeaponConfigSO")]
public class WeaponConfigSO : ScriptableObject {
    [Header("GENERAL")]
    public string weaponName;
    public Weapon weaponPrefab;
    public Sprite weaponIcon;
    public bool unlocked;
    
    [Space(10)]
    [Header("STATS")]
    public int armorStatUpgradeValue = 0;
    public int strengthStatUpgradeValue = 0;

    public int maxLifeStatUpgradeValue = 0;

    public int recoveryStatUpgradeValue = 0;

    public int criticalHitChanceStatUpgradeValue = 0;
    public int criticalDamageStatUpgradeValue = 0;

    public void InitialiseValues()
    {
        StatsManager.Instance.GetStrengthStat.Upgrade(strengthStatUpgradeValue);
        StatsManager.Instance.GetArmorStat.Upgrade(armorStatUpgradeValue);
        StatsManager.Instance.GetMaxHealthStat.Upgrade(maxLifeStatUpgradeValue);
        StatsManager.Instance.GetRecoveryStat.Upgrade(recoveryStatUpgradeValue);
        StatsManager.Instance.GetCriticalHitChanceStat.Upgrade(criticalHitChanceStatUpgradeValue);
        StatsManager.Instance.GetCriticalHitDamageStat.Upgrade(criticalDamageStatUpgradeValue);
    }
}