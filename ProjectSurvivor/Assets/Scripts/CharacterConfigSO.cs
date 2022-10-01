using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterConfig", menuName = "ScriptableObjects/Player/CharacterConfig")]
public class CharacterConfigSO : ScriptableObject
{
    [Header("GENERAL INFO")]
    public string characterName;
    public Sprite characterIcon;
    public GameObject modelPrefab;
    public AbilityDataSO startingAbility;
    public WeaponConfigSO equippedWeapon;
    public WeaponConfigSO[] weapons;

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

    [Header("STATS")]
    [Header("MAX HEALTH STAT CONFIG")]
    public MaxHealthStatConfig maxHealthStatConfig;
    [Header("RECOVERY STAT CONFIG")]
    public RecoveryStatConfig recoveryStatConfig;
    [Header("ARMOR STAT CONFIG")]
    public ArmorStatConfig armorStatConfig;
    [Header("STRENGTH STAT CONFIG")]
    public StrengthStatConfig strengthStatConfig;
    [Header("CRITICAL HIT CHANCE STAT CONFIG")]
    public CriticalHitChanceStatConfig criticalHitChanceStatConfig;
    [Header("CRITICAL HIT DAMAGE STAT CONFIG")]
    public CriticalHitDamageStatConfig criticalHitDamageStatConfig;
}
