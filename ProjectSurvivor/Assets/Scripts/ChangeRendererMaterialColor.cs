using UnityEngine;

public class ChangeRendererMaterialColor : MonoBehaviour
{
    [SerializeField] 
    private Renderer myRenderer = null;
    [SerializeField] 
    private bool useEmission = false;
    [SerializeField]
    private Color hdrColor = Color.white;

    private void Start()
    {
        if (!myRenderer)
        {
            myRenderer = GetComponent<Renderer>();
        }

        ChangeColors();
    }

    private void ChangeColors()
    {
        if (useEmission)
        {
            myRenderer.material.SetColor("_EmissionColor", hdrColor);
        }
    }
}
