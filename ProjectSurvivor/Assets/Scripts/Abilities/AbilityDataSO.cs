using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData_New", menuName = "ScriptableObjects/Ability/AbilityData")]
public class AbilityDataSO : ScriptableObject
{
    public string weaponName;
    public int maxLevel;
    public AbilityStats abilityStats;
    public AbilityBase abilityPrefab;
    public List<UpgradeDataSO> upgradesByLevel;

    [Header("EFFECT")]
    public int chanceToApplyEffect = 100;
    public float effectApplyCooldown = 1f;
    public EffectBaseSO effect;

    public AbilityBase AbilityInstance
    {
        get
        {
            return abilityInstance;
        }
        set
        {
            abilityInstance = value;
        }
    }

    private AbilityBase abilityInstance;

    public UpgradeDataSO UpgradeByLevel(int currentLevel)
    {
        return upgradesByLevel[currentLevel - 1];
    }
}
