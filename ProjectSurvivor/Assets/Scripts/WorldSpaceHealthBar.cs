using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image barFillImage;
    [SerializeField]
    private GameObject barBackground;
    [SerializeField]
    private float fillLerpTime = 0.1f;
    [SerializeField]
    private Health health;


    private void OnEnable()
    {
        if(health.GetHealthFraction() == 1.0f)
        {
            barBackground.SetActive(false);
        }

        health.OnTakeDamage += RefreshHealthBar;
    }


    private void OnDisable()
    {
        health.OnTakeDamage -= RefreshHealthBar;
    }

    private void RefreshHealthBar(int amount, bool isCritical, bool isDamageOverTime)
    {
        if (health.GetHealthFraction() != 1.0f)
            barBackground.SetActive(true);

        StartCoroutine(RefreshExperienceBarRoutine());
    }

    private IEnumerator RefreshExperienceBarRoutine()
    {
        float startTime = Time.time;
        float previousFillAmount = barFillImage.fillAmount;

        while (Time.time < startTime + fillLerpTime)
        {
            barFillImage.fillAmount = Mathf.Lerp(previousFillAmount,
                health.GetHealthFraction(),
                (Time.time - startTime) / fillLerpTime);

            yield return null;
        }
    }
}
