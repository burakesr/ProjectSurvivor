using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI damageText;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private Color textColor;

    [Header("TWEENING VALUES")]
    [SerializeField]
    private Vector3 moveVector;
    [SerializeField]
    private float moveDuration;
    [SerializeField]
    private float scaleDuration;

    private float m_lifeTimer;

    private void OnEnable()
    {
        m_lifeTimer = lifeTime; 
    }

    public void Setup(int damageAmount)
    {
        damageText.SetText(damageAmount.ToString());
        damageText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(transform.position + moveVector, moveDuration, false));
        sequence.Join(damageText.DOFade(1f, 0.25f));

        sequence.Play();
    }

    private void Update()
    {
        m_lifeTimer -= Time.deltaTime;
        if (m_lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
