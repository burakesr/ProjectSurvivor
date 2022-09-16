using UnityEngine;

public class EffectUIHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject holderTransform;
    [SerializeField]
    private EffectStatusUI effectStatusPrefab;

    public EffectStatusUI CreateEffectStatus(Sprite effectIcon)
    {
        EffectStatusUI effectStatusUI = PoolManager.Instance.SpawnFromPool(effectStatusPrefab.gameObject, Vector3.zero,
            Quaternion.identity).GetComponent<EffectStatusUI>();

        RectTransform rectTransform = effectStatusUI.GetComponent<RectTransform>();
        effectStatusUI.transform.SetParent(holderTransform.transform);

        rectTransform.localPosition = Vector3.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;

        effectStatusUI.SetIcon(effectIcon);

        return effectStatusUI;
    }

}
