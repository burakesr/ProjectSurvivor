using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected EnemyStatsConfigSO stats;
    [SerializeField]
    protected Transform aimPoint = null;
    [SerializeField]
    protected Transform damagePopupSpawnTransform = null;

    [Header("EFFECTS & VISUALS")]
    [SerializeField]
    private ParticleSystem[] secondContactDustEffect;
    [SerializeField]
    private ParticleSystem[] firstContactDustEffects;


    public Transform GetAimPoint => aimPoint;
    public Health GetHealth => p_health;
    public NavMeshAgent GetAgent => p_agent;
    public AIMovementControl GetMovementControl => p_movementControl;
    public Player GetPlayer => p_player;
    public Transform GetDamagePopupSpawnTransform => damagePopupSpawnTransform;

    protected Health p_health;
    protected DropItemOnDestroy p_dropItemOnDestroy;
    protected NavMeshAgent p_agent;
    protected Player p_player;
    protected AIMovementControl p_movementControl;
    protected AnimatorController p_animController;

    protected float p_contactDamageTimer;
    protected float p_attackTimer;
    protected float p_agentStopDelayTimer;
    protected float p_chaseWaitTimer;

    protected const float p_agentStopDelay = 0.5f;

    private void Awake()
    {
        p_health = GetComponent<Health>();
        p_agent = GetComponent<NavMeshAgent>();
        p_dropItemOnDestroy = GetComponent<DropItemOnDestroy>();
        p_movementControl = GetComponent<AIMovementControl>();
        p_animController = GetComponent<AnimatorController>();
    }

    private void Start()
    {
        StatsInitialisation();

        p_player = GameManager.Instance.GetPlayer();
    }

    private void OnEnable()
    {
        //p_health.OnTakeDamage += OnTakeDamage;
        p_health.OnDie += Die;
        p_agent.enabled = true;

        p_health.SetStartingHealth(stats.maxHealth);
    }

    private void OnDisable()
    {
        //p_health.OnTakeDamage -= OnTakeDamage;
        p_health.OnDie -= Die;
    }

    private void Update()
    {
        p_contactDamageTimer -= Time.deltaTime;
        p_attackTimer -= Time.deltaTime;
        p_agentStopDelayTimer -= Time.deltaTime;
        p_chaseWaitTimer -= Time.deltaTime;

        p_movementControl.MoveTowardsPlayer();

        AnimatonHandler();
    }

    private void AnimatonHandler()
    {
        if (p_agent.velocity.sqrMagnitude >= 0.01f)
        {
            p_animController.OnCharacterWalking();
        }
        else if (p_agent.velocity.sqrMagnitude <= 0.01f)
        {
            p_animController.OnCharacterIdle();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionDamageToPlayer(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionDamageToPlayer(collision);
    }

    private void StatsInitialisation()
    {
        p_agent.speed = stats.speed;
        p_agent.angularSpeed = stats.angularSpeed;
        p_agent.acceleration = stats.acceleration;
        p_agent.stoppingDistance = stats.stoppingDistance;
        p_agent.autoBraking = stats.autoBraking;
    }

    private void CollisionDamageToPlayer(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player && p_contactDamageTimer <= 0)
        {
            player.GetHealth.TakeArmoredDamage(stats.contactDamage);
            p_contactDamageTimer = stats.damageInterval;
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);

        EnemySpawnManager.Instance.OnRemoveToEnemiesList(this);
        GameManager.Instance.UpdateKillCount();
        p_dropItemOnDestroy.DropItem();
    }

    public void SecondContactDustEffects(){
        foreach (var effect in secondContactDustEffect)
        {
            effect.Play();
        }
    }

    public void FirstContactDustEffects(){
        foreach (var effect in firstContactDustEffects)
        {
            effect.Play();
        }
    }
}
