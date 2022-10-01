using UnityEngine;

public class ChangeRendererMaterialColor : MonoBehaviour
{
    [SerializeField] 
    private Renderer myRenderer = null;
    [SerializeField] 
    private bool useEmission = false;
    [SerializeField]
    private Color hdrColor = Color.white;

    private Material m_material;

    private void Awake() 
    {
        if (!myRenderer)
        {
            myRenderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        m_material = myRenderer.material;

        ChangeColors();
    }

    private void ChangeColors()
    {
        if (useEmission)
        {
            m_material.SetColor("_EmissionColor", hdrColor);
        }
    }
}
