using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrade_ProjectileCountUpgrade", menuName = "ScriptableObjects/AbilityUpgrades/Projectile Count")]
public class ProjectileCountUpgradeSO : AbilityUpgradeSO
{
    public int addedProjectileCount;
    public override void Upgrade(AbilityDataSO abilityData)
    {
        abilityData.AbilityInstance.projectileCount += addedProjectileCount;
    }
}