using UnityEngine;

[CreateAssetMenu(fileName = "MaterialSO", menuName = "ScriptableObjects/Materials/Material", order = 0)]
public class MaterialSO : ScriptableObject 
{
    public Sprite icon;
    public MaterialType type;
}

public enum MaterialType
{
    MindSilver,
    Taragotium,
    Orichalcum
}