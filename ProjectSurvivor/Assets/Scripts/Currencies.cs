using System;
using TMPro;
using UnityEngine;

public class Currencies : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI material1Text;
    [SerializeField]
    private TextMeshProUGUI material2Text;
    [SerializeField]
    private TextMeshProUGUI material3Text;

    private void Start() {
        UpdateMaterialTexts();
    }

    private void UpdateMaterialTexts()
    {
        material1Text.text = GameManager.Instance.PlayerStats.taragotiumMaterialCount.ToString();
        material2Text.text = GameManager.Instance.PlayerStats.mindSilverMaterialCount.ToString();
        material3Text.text = GameManager.Instance.PlayerStats.orichalcumMaterialCount.ToString();
    }
}
