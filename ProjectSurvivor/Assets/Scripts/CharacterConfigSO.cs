using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterConfig", menuName = "ScriptableObjects/Player/CharacterConfig")]
public class CharacterConfigSO : ScriptableObject
{
    [Header("GENERAL INFO")]
    public string characterName;
    public Sprite characterIcon;

    [Header("CHARACTER INFO")]
    public float height = 1.8f;
    public float radius = 0.3f;

    [Header("MOVEMENT INFO")]
    public float moveSpeed;
    [Range(0f, 1f)]
    public float turnSmoothness;
    public float slopeLimit = 60f;

    [Header("GROUND CHECK")]
    public float gravityForce = -20f;
    public LayerMask groundCheckLayer;
    public float groundCheckBuffer = 0.5f;
    public float groundCheckRadiusBuffer = 0.05f;
}
