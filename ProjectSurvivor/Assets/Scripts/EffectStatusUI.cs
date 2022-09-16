using UnityEngine;
using UnityEngine.UI;

public class EffectStatusUI : MonoBehaviour
{
    public EffectType effectType;
    [SerializeField]
    private Image statusIcon;
    [SerializeField]
    private Image effectDuration;

    private Transform poolTransform;

    private void OnEnable()
    {
        poolTransform = transform.parent;
    }

    public void UpdateEffectDurationImage(float duration, float remainingTime)
    {
        effectDuration.fillAmount = 1f / (remainingTime / duration);
    }

    public void SetIcon(Sprite iconSprite)
    {
        statusIcon.sprite = iconSprite;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        transform.SetParent(poolTransform);
    }
}
