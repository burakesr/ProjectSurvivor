using System.Collections;
using UnityEngine;

public class HitFlashEffect : MonoBehaviour 
{
    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color flashColor = Color.red;
    [SerializeField]
    private float flashDuration = 0.25f;

    private float flashTimer;

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Awake() 
    {
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public IEnumerator FlashRoutine()
    {
        _skinnedMeshRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        _skinnedMeshRenderer.material.color = defaultColor;
    }
}