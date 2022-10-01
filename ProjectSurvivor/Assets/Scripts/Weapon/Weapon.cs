using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void InitialiseWeapon(WeaponConfigSO weaponConfig)
    {
        weaponConfig.InitialiseValues();
    }
}