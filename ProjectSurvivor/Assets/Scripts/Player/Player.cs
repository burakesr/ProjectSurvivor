using UnityEngine;

#region Required Components
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AbilityManager))]
[RequireComponent(typeof(PlayerInputControl))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatorController))]
[RequireComponent(typeof(PlayerMovementControl))]
[RequireComponent(typeof(UpgradesManager))]
[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(Health))]
#endregion

public class Player : MonoBehaviour
{
    [Header("GENERAL")]
    public CharacterConfigSO CharacterConfig;

    public Rigidbody GetRigidbody => _rb;
    public AbilityManager GetAbilityManager => _abilityManager;
    public UpgradesManager GetUpgradesManager => _upgradesManager;
    public PlayerInputControl GetInputControl => _input;
    public Animator GetAnimator => _animator;
    public AnimatorController GetAnimationControl => _animationControl;
    public PlayerMovementControl GetMovementControl => _movementControl;
    public LevelManager GetLevelManager => _levelManager;
    public GetNearestEnemyToThePlayer GetNearestEnemyToThePlayer => _nearestEnemyToThePlayer;
    public Health GetHealth => _health;

    private Rigidbody _rb;
    private AbilityManager _abilityManager;
    private PlayerInputControl _input;
    private Animator _animator;
    private AnimatorController _animationControl;
    private PlayerMovementControl _movementControl;
    private UpgradesManager _upgradesManager;
    private LevelManager _levelManager;
    private Health _health;
    private GetNearestEnemyToThePlayer _nearestEnemyToThePlayer;
    private HitFlashEffect _hitFlashEffect;
    
    private float _recoveryTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animationControl = GetComponent<AnimatorController>();
        _movementControl = GetComponent<PlayerMovementControl>();
        _input = GetComponent<PlayerInputControl>();
        _abilityManager = GetComponent<AbilityManager>();
        _upgradesManager = GetComponent<UpgradesManager>();
        _levelManager = GetComponent<LevelManager>();
        _health = GetComponent<Health>();
        _nearestEnemyToThePlayer = GetComponent<GetNearestEnemyToThePlayer>();
        _hitFlashEffect = GetComponent<HitFlashEffect>();
    }

    private void OnEnable() 
    {
        _health.OnTakeDamage += TakeDamage;
    }

    private void OnDisable() 
    {
        _health.OnTakeDamage += TakeDamage;
    }

    private void Update() 
    {
        _recoveryTimer -= Time.deltaTime;

        if (_recoveryTimer < 0){
            StatsManager.Instance.GetRecoveryStat.Action(_health);
            _recoveryTimer = StatsManager.Instance.GetRecoveryStat.recoveryTime;
        }
    }

    private void TakeDamage(int amount, bool isCritical, bool isDamageOverTime)
    {
        StopCoroutine(_hitFlashEffect.FlashRoutine());
        StartCoroutine(_hitFlashEffect.FlashRoutine());
    }
}
