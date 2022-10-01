using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] 
    private Transform abilityHolder;

    private List<AbilityDataSO> _abilities = new List<AbilityDataSO>();

    public List<AbilityDataSO> GetAbilities => _abilities;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        AddAbility(_player.CharacterConfig.startingAbility);
    }

    public void AddAbility(AbilityDataSO ability)
    {
        if (!_abilities.Contains(ability))
        {
            _abilities.Add(ability);

            AbilityBase weaponBase = Instantiate(ability.abilityPrefab, abilityHolder);
            weaponBase.SetData(ability);
            weaponBase.LevelUp();
            ability.AbilityInstance = weaponBase;

            _player.GetUpgradesManager.AddUpgradeToAvailabeUpgradesList(ability.UpgradeByLevel(weaponBase.GetCurrentLevel));
        }
    }
}
