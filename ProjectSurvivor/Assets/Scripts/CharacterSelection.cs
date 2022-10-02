using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private Transform characterSelectionPlatform;
    [SerializeField]
    private CharacterConfigSO[] characters;
    [SerializeField]
    private Transform characterButtonsHolder;
    [SerializeField]
    private Button characterSelectionBtnPrefab;
    [SerializeField]
    private Transform weaponButtonsHolder;
    [SerializeField]
    private TextMeshProUGUI weaponNameText;
    [SerializeField]
    private TextMeshProUGUI weaponInfoText;
    [SerializeField]
    private Button weaponSelectionBtnPrefab;
    [SerializeField]
    private Button weaponEquipBtnPrefab;
    [SerializeField]
    private Button weaponEquippedBtnPrefab;
    [SerializeField]
    private Button weaponUnlockBtnPrefab;

    private List<Button> _currentWeaponButtons = new List<Button>();
    private CharacterConfigSO _currentSelectedCharacter;
    private GameObject _characterModel;
    private WeaponConfigSO _currentSelectedWeapon;

    private void Start() {
        AddCharactersToHolder();
    }

    private void AddCharactersToHolder(){
        foreach (var character in characters)
        {
            Button characterSelectionButton = Instantiate(characterSelectionBtnPrefab, characterButtonsHolder);
            characterSelectionButton.GetComponent<Image>().sprite = character.characterIcon;
            
            characterSelectionButton.onClick?.AddListener(() => AddWeaponsToHolder(character));
        }

        AddWeaponsToHolder(characters[0]);
    }

    private void AddWeaponsToHolder(CharacterConfigSO character){
        
        if (_currentSelectedCharacter == character) return;

        ChangeCurrentCharacter(character);
        
        for (int i = 0; i < _currentWeaponButtons.Count; i++)
        {
            Destroy(_currentWeaponButtons[i].gameObject);
        }

        _currentWeaponButtons.Clear();
        _currentSelectedWeapon = character.equippedWeapon;

        foreach (var weapon in character.weapons)
        {
            Button weaponSelectionButton = Instantiate(weaponSelectionBtnPrefab, weaponButtonsHolder);
            weaponSelectionButton.GetComponent<Image>().sprite = weapon.weaponIcon;

            weaponSelectionButton.onClick?.AddListener(() => ClearText());
            weaponSelectionButton.onClick?.AddListener(() => ShowWeaponInfo(weapon));
            
            _currentWeaponButtons.Add(weaponSelectionButton);
        }

        _currentWeaponButtons[0].onClick.Invoke();
    }

    private void ShowWeaponInfo(WeaponConfigSO weapon)
    {
        weaponNameText.text = weapon.weaponName;

        ChangeWeaponInfoText(weapon.armorStatUpgradeValue, "Armor", false);
        ChangeWeaponInfoText(weapon.maxLifeStatUpgradeValue, "Max Life", false);
        ChangeWeaponInfoText(weapon.recoveryStatUpgradeValue, "Recovery", false);
        ChangeWeaponInfoText(weapon.strengthStatUpgradeValue, "Strength", false);
        ChangeWeaponInfoText(weapon.criticalHitChanceStatUpgradeValue, "Critical Chance", true);
        ChangeWeaponInfoText(weapon.criticalDamageStatUpgradeValue, "Critical Damage", true);

        weaponEquipBtnPrefab.onClick?.AddListener(() => ChangeCurrentCharacterEquippedWeapon(weapon));
        weaponEquipBtnPrefab.onClick?.AddListener(() => HandleWeaponButtons(weapon));
        
        weaponUnlockBtnPrefab.onClick?.AddListener(() => UnlockWeapon(weapon));
        weaponUnlockBtnPrefab.onClick?.AddListener(() => HandleWeaponButtons(weapon));

        HandleWeaponButtons(weapon);
    }

    private void UnlockWeapon(WeaponConfigSO weapon)
    {
        //Todo check if have enough materials
        weapon.unlocked = true;
    }

    private void HandleWeaponButtons(WeaponConfigSO weapon)
    {
        if (weapon.unlocked)
        {
            if (weapon == _currentSelectedCharacter.equippedWeapon)
            {
                weaponEquippedBtnPrefab.gameObject.SetActive(true);
                weaponUnlockBtnPrefab.gameObject.SetActive(false);
                weaponEquipBtnPrefab.gameObject.SetActive(false);
            }
            else
            {
                weaponEquippedBtnPrefab.gameObject.SetActive(false);
                weaponUnlockBtnPrefab.gameObject.SetActive(false);
                weaponEquipBtnPrefab.gameObject.SetActive(true);
            }
        }
        else
        {
            weaponUnlockBtnPrefab.gameObject.SetActive(true);
            weaponEquipBtnPrefab.gameObject.SetActive(false);
            weaponEquippedBtnPrefab.gameObject.SetActive(false);
        }
    }

    private void ChangeWeaponInfoText(int statValue, string statName, bool percentageBonus){        
        if (statValue != 0)
        {
            string text = new string("");
            if (statValue < 0)
            {
                if (percentageBonus)
                {
                    text = "<color=red>-%" + Mathf.Abs(statValue) + "</color>" + " " + statName + "\n";
                }
                else
                {
                    text = "<color=red>-" + Mathf.Abs(statValue) + "</color>" +  " " + statName + "\n";
                }
            }
            else
            {
                if (percentageBonus)
                {
                    text = "<color=green>+%" + statValue + "</color>" + " " + statName + "\n";
                }
                else
                {
                    text = "<color=green>+" + statValue + "</color>" + " " + statName + "\n";
                }
            }

            weaponInfoText.text =  weaponInfoText.text + text;
        }
    }

    private void ChangeCurrentCharacterEquippedWeapon(WeaponConfigSO weapon){
        _currentSelectedCharacter.equippedWeapon = weapon;
    }

    private void ChangeCurrentCharacter(CharacterConfigSO character){
        _currentSelectedCharacter = character;
        Destroy(_characterModel);
        _characterModel = Instantiate(_currentSelectedCharacter.modelPrefab, characterSelectionPlatform);
        GameManager.Instance.PlayerPrefab = character.playerPrefab;
    }

    private void ClearText(){
        weaponInfoText.text = "";
    }
}
