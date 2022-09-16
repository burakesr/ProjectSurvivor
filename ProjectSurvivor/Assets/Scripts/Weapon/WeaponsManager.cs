using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private WeaponDataSO[] startingWeapons;

    private List<WeaponDataSO> weapons = new List<WeaponDataSO>();

    public List<WeaponDataSO> GetWeapons => weapons;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }


    private void Start()
    {
        foreach (var weapon in startingWeapons)
        {
            AddWeapon(weapon);
        }
    }

    public void AddWeapon(WeaponDataSO weapon)
    {
        if (!weapons.Contains(weapon))
        {
            weapons.Add(weapon);

            WeaponBase weaponBase = Instantiate(weapon.weaponPrefab, weaponHolder);
            weaponBase.SetData(weapon);
            weaponBase.LevelUp();
            weapon.WeaponInstance = weaponBase;

            player.GetUpgradesManager.AddUpgradeToAvailabeUpgradesList(weapon.UpgradeByLevel(weaponBase.GetCurrentLevel));
        }
    }
}
