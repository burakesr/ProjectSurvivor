using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] 
    private TextMeshPro damageText;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private Color normalDamageTextColor;
    [SerializeField]
    private float normalDamageTextFontSize = 18f;
    [SerializeField]
    private Color criticalDamageTextColor;
    [SerializeField]
    private float criticalDamageTextFontSize = 24f;
    [SerializeField]
    private Color damageOvertimeTextColor;
    [SerializeField]
    private float damageOvertimeextFontSize = 12f;

    [Header("TWEENING VALUES")]
    [SerializeField]
    private Vector3 moveVector;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float scaleDuration;

    private float _lifeTimer;

    private static int _sortingOder;

    private void OnEnable()
    {
        _lifeTimer = lifeTime; 
    }

    public void Setup(int damageAmount, bool isCritical, bool isDamageOverTime)
    {
        damageText.SetText(damageAmount.ToString());
        
        if (isCritical)
        {
            damageText.color = criticalDamageTextColor;
            damageText.fontSize = criticalDamageTextFontSize;
        }
        else if (isDamageOverTime)
        {
            damageText.color = damageOvertimeTextColor;
            damageText.fontSize = damageOvertimeextFontSize;
        }
        else
        {
            damageText.color = normalDamageTextColor;
            damageText.fontSize = normalDamageTextFontSize;
        }

        damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 0f);
        
        _sortingOder++;
        damageText.sortingOrder = _sortingOder;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + moveVector, moveDuration, false));
        sequence.Join(damageText.DOFade(1f, 0.25f));

        sequence.Play();
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
