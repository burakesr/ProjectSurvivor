using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade_New", menuName = "ScriptableObjects/WeaponUpgrades/Damage Upgrade")]
public class DamageUpgradeSO : WeaponUpgradeSO
{
    public WeaponStats[] statsByLevel;

    public override void Upgrade(WeaponDataSO weaponData)
    {
        WeaponStats newStats = statsByLevel[weaponData.WeaponInstance.GetCurrentLevel - 2];

        weaponData.WeaponInstance.GetWeaponStats = newStats;

        Debug.Log("Upgrade Applied!");
    }
}
