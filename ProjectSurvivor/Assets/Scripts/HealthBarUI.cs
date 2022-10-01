using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        RefreshHealthBar(0);
    }

    private void OnEnable()
    {
        GameManager.Instance.GetPlayer().GetHealth.OnTakeDamage += RefreshHealthBar;
        GameManager.Instance.GetPlayer().GetHealth.OnHeal += RefreshHealthBar;
    }

    private void OnDisable()
    {
        GameManager.Instance.GetPlayer().GetHealth.OnTakeDamage -= RefreshHealthBar;
        GameManager.Instance.GetPlayer().GetHealth.OnHeal -= RefreshHealthBar;
    }

    private void RefreshHealthBar(int amount, bool isCritical, bool isDamageOverTime)
    {
        Health health = GameManager.Instance.GetPlayer().GetHealth;

        healthText.text = health.GetCurrentHealth.ToString() + "/" + health.GetMaxHealth.ToString();
        fillImage.fillAmount = health.GetHealthFraction();
    }

    private void RefreshHealthBar(int amount)
    {
        Health health = GameManager.Instance.GetPlayer().GetHealth;

        healthText.text = health.GetCurrentHealth.ToString() + "/" + health.GetMaxHealth.ToString();
        fillImage.fillAmount = health.GetHealthFraction();
    }
}
