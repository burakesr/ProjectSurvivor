using UnityEngine;

public abstract class WeaponUpgradeSO : ScriptableObject
{
    public abstract void Upgrade(WeaponDataSO weaponData);
}