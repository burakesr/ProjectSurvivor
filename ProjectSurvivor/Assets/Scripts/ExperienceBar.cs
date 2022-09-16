using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField]
    private Image levelFillImage = null;
    [SerializeField]
    private TextMeshProUGUI levelText = null;
    [SerializeField]
    private float fillLerpTime = 0.5f;

    private void Start()
    {
        RefreshExperienceBar();
    }

    private void OnEnable()
    {
        GameManager.Instance.GetPlayer().GetLevelManager.levelData.OnExperienceGained += RefreshExperienceBar;
    }

    private void OnDisable()
    {
        GameManager.Instance.GetPlayer().GetLevelManager.levelData.OnExperienceGained += RefreshExperienceBar;
    }

    private void RefreshExperienceBar()
    {
        LevelManager levelManager = GameManager.Instance.GetPlayer().GetLevelManager;

        levelText.text = "LVL: " + levelManager.GetCurrentLevel.ToString();

        levelFillImage.fillAmount = GameManager.Instance.GetPlayer().GetLevelManager.GetExperienceFraction();

        StartCoroutine(RefreshExperienceBarRoutine());
    }

    private IEnumerator RefreshExperienceBarRoutine()
    {
        float startTime = Time.time;
        float previousFillAmount = levelFillImage.fillAmount;

        while (Time.time < startTime + fillLerpTime)
        {
            levelFillImage.fillAmount = Mathf.Lerp(previousFillAmount, 
                GameManager.Instance.GetPlayer().GetLevelManager.GetExperienceFraction(),
                (Time.time - startTime) / fillLerpTime);
        
            yield return null;
        }
    }
}
