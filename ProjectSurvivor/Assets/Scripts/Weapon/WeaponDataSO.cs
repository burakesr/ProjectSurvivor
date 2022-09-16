using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/Weapon/New WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;
    public int maxLevel;
    public WeaponStats weaponStats;
    public WeaponBase weaponPrefab;
    public List<UpgradeDataSO> upgradesByLevel;

    [Header("EFFECT")]
    public int chanceToApplyEffect = 100;
    public float effectApplyCooldown = 1f;
    public EffectBaseSO effect;

    public WeaponBase WeaponInstance
    {
        get
        {
            return weaponInstance;
        }
        set
        {
            weaponInstance = value;
        }
    }

    private WeaponBase weaponInstance;

    public UpgradeDataSO UpgradeByLevel(int currentLevel)
    {
        return upgradesByLevel[currentLevel - 1];
    }
}
