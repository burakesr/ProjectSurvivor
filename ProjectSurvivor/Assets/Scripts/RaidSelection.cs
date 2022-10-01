using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidSelection : MonoBehaviour
{
    [SerializeField]
    private Transform mapSelectionContainer;
    [SerializeField]
    private GameObject mapSelectionSlotPrefab;
    [SerializeField]
    private TextMeshProUGUI mapNameText;
    [SerializeField]
    private Transform materialsContaier;
    [SerializeField]
    private GameObject materialSlotPrefab;
    [SerializeField]
    private Transform encounterSlotContainer;
    [SerializeField]
    private GameObject encounterSlotPrefab;
    [SerializeField]
    private List<LevelDataSO> levels = new List<LevelDataSO>();

    private List<GameObject> materialSlots = new List<GameObject>();
    private List<GameObject> encounterSlots = new List<GameObject>();
    private List<GameObject> mapSelectionSlots = new List<GameObject>();

    private LevelDataSO selectedLevel;

    private void OnEnable() {
        selectedLevel = levels[0];

        CreateLevelSlots();

        SetEncounterSlots(selectedLevel);
        SetMaterialSlots(selectedLevel);
        SetMapNameText(selectedLevel.levelName);
    }

    private void OnDisable() {
        
    }

    private void CreateLevelSlots()
    {
        ClearSlots(mapSelectionSlots);
        foreach (var level in levels)
        {
            GameObject mapSelectionSlot = Instantiate(mapSelectionSlotPrefab, mapSelectionContainer);
            
            mapSelectionSlot.GetComponent<Image>().sprite = level.levelIcon;
            mapSelectionSlots.Add(mapSelectionSlot);

            Button button = mapSelectionSlot.GetComponent<Button>();
            button.onClick.AddListener(() => SetMapNameText(level.levelName));
            button.onClick.AddListener(() => SetEncounterSlots(level));
            button.onClick.AddListener(() => SetMaterialSlots(level));
        }
    }

    private void DebugButtonTest(string text){
        Debug.Log(text);
    }

    private void SetMapNameText(string mapName){
        mapNameText.text = mapName;
    }

    private void SetEncounterSlots(LevelDataSO level){
        selectedLevel = level;
        ClearSlots(encounterSlots);

        foreach (var encounter in level.enemies)
        {
            GameObject encounterSlot = Instantiate(encounterSlotPrefab, encounterSlotContainer);
            encounterSlot.GetComponent<Image>().sprite = encounter.icon; 

            encounterSlots.Add(encounterSlot);
        }
    }

    private void SetMaterialSlots(LevelDataSO level){
        selectedLevel = level;
        ClearSlots(materialSlots);

        foreach (var material in level.availableMaterials)
        {
            GameObject materialSlot = Instantiate(materialSlotPrefab, materialsContaier);
            materialSlot.GetComponent<Image>().sprite = material.icon;

            materialSlots.Add(materialSlot);
        }
    }

    private void ClearSlots(List<GameObject> slots){
        if (slots.Count < 1) return;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
            {
                Destroy(slots[i]);
            }
        }
    }

    public void LoadRaidScene(){
        MySceneManager.Instance.LoadGame(selectedLevel.levelScene);
    }
}