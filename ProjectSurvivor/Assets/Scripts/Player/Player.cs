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

    public Rigidbody GetRigidbody => rb;
    public AbilityManager GetAbilityManager => abilityManager;
    public UpgradesManager GetUpgradesManager => upgradesManager;
    public PlayerInputControl GetInputControl => input;
    public Animator GetAnimator => animator;
    public AnimatorController GetAnimationControl => animationControl;
    public PlayerMovementControl GetMovementControl => movementControl;
    public LevelManager GetLevelManager => levelManager;
    public GetNearestEnemyToThePlayer GetNearestEnemyToThePlayer => nearestEnemyToThePlayer;
    public Health GetHealth => health;

    private Rigidbody rb;
    private AbilityManager abilityManager;
    private PlayerInputControl input;
    private Animator animator;
    private AnimatorController animationControl;
    private PlayerMovementControl movementControl;
    private UpgradesManager upgradesManager;
    private LevelManager levelManager;
    private Health health;
    private GetNearestEnemyToThePlayer nearestEnemyToThePlayer;

    private float _recoveryTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animationControl = GetComponent<AnimatorController>();
        movementControl = GetComponent<PlayerMovementControl>();
        input = GetComponent<PlayerInputControl>();
        abilityManager = GetComponent<AbilityManager>();
        upgradesManager = GetComponent<UpgradesManager>();
        levelManager = GetComponent<LevelManager>();
        health = GetComponent<Health>();
        nearestEnemyToThePlayer = GetComponent<GetNearestEnemyToThePlayer>();
    }

    private void Update() {
        _recoveryTimer -= Time.deltaTime;

        if (_recoveryTimer < StatsManager.Instance.GetRecoveryStat.recoveryTime){
            StatsManager.Instance.GetRecoveryStat.Action(health);
        }
    }
}
