using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public Transform weaponSpawnPosition;

    private Weapon _currentWeapon;

    private Player _player;

    private void Awake() {
        _player = GetComponent<Player>();
    }

    private void Start() {
        InitialiseWeapon();
    }

    private void InitialiseWeapon()
    {
        _currentWeapon = Instantiate(_player.CharacterConfig.equippedWeapon.weaponPrefab, weaponSpawnPosition);
        _currentWeapon.InitialiseWeapon(_player.CharacterConfig.equippedWeapon);
    }
}