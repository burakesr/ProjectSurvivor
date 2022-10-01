using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade_New", menuName = "ScriptableObjects/AbilityUpgrades/Damage Upgrade")]
public class DamageUpgradeSO : AbilityUpgradeSO
{
    public AbilityStats[] statsByLevel;

    public override void Upgrade(AbilityDataSO weaponData)
    {
        AbilityStats newStats = statsByLevel[weaponData.AbilityInstance.GetCurrentLevel - 2];

        weaponData.AbilityInstance.GetWeaponStats = newStats;

        Debug.Log("Upgrade Applied!");
    }
}
