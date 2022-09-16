using UnityEngine;

#region Required Components
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WeaponsManager))]
[RequireComponent(typeof(PlayerInputControl))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatorController))]
[RequireComponent(typeof(PlayerMovementControl))]
[RequireComponent(typeof(UpgradesManager))]
[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(StatsManager))]
[RequireComponent(typeof(Health))]
#endregion

public class Player : MonoBehaviour
{
    public SoundEffect experienceGainedSFX;

    public Rigidbody GetRigidbody => rb;
    public WeaponsManager GetWeaponsManager => weaponsManager;
    public UpgradesManager GetUpgradesManager => upgradesManager;
    public PlayerInputControl GetInputControl => input;
    public Animator GetAnimator => animator;
    public AnimatorController GetAnimationControl => animationControl;
    public PlayerMovementControl GetMovementControl => movementControl;
    public LevelManager GetLevelManager => levelManager;
    public StatsManager GetStatsManager => statsManager;
    public GetNearestEnemyToThePlayer GetNearestEnemyToThePlayer => nearestEnemyToThePlayer;
    public Health GetHealth => health;

    private Rigidbody rb;
    private WeaponsManager weaponsManager;
    private PlayerInputControl input;
    private Animator animator;
    private AnimatorController animationControl;
    private PlayerMovementControl movementControl;
    private UpgradesManager upgradesManager;
    private LevelManager levelManager;
    private StatsManager statsManager;
    private Health health;
    private GetNearestEnemyToThePlayer nearestEnemyToThePlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animationControl = GetComponent<AnimatorController>();
        movementControl = GetComponent<PlayerMovementControl>();
        input = GetComponent<PlayerInputControl>();
        weaponsManager = GetComponent<WeaponsManager>();
        upgradesManager = GetComponent<UpgradesManager>();
        levelManager = GetComponent<LevelManager>();
        statsManager = GetComponent<StatsManager>();
        health = GetComponent<Health>();
        nearestEnemyToThePlayer = GetComponent<GetNearestEnemyToThePlayer>();
    }
}
