using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected enum EnemyState
    {
        Chase,
        TargetInRange,
        Attack,
    }

    [SerializeField]
    protected EnemyStatsConfigSO stats;
    [SerializeField]
    protected Transform aimPoint = null;
    [SerializeField]
    protected Transform damagePopupSpawnTransform = null;
    [SerializeField]
    protected EnemyState currentState;

    [Space(10)]
    [Header("ATTACK VARIABLES")]
    [SerializeField]
    private float waitBeforeAttackAnim = 0.2f;
    [SerializeField]
    private float waitAfterAttackAnim = 1f;
    [SerializeField]
    protected AttackTrigger attackTrigger = null;

    [Space(10)]
    [Header("TARGETING")]
    [SerializeField]
    private float rotateTowardsTargetSpeed = 20f;
    [SerializeField]
    private RotationDirection m_rotationDirection = RotationDirection.NoRotation;
    [SerializeField]
    private bool canRotateWhileAttacking = false;
    [SerializeField]
    private float chaseWaitTime = 0.3f;

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

        m_isAttacking = false;
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

        bool targetInRangeOfAttack = Vector3.Distance(transform.position, p_player.transform.position) < stats.attackRange;

        AIStateHandler(targetInRangeOfAttack);
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

    private void AIStateHandler(bool targetInRangeOfAttack)
    {
        if (targetInRangeOfAttack && p_attackTimer < 0f)
        {
            currentState = EnemyState.Attack;
        }
        else if (targetInRangeOfAttack && p_attackTimer > 0f)
        {
            currentState = EnemyState.TargetInRange;
        }
        else if (!targetInRangeOfAttack && !m_isAttacking)
        {
            currentState = EnemyState.Chase;
        }

        switch (currentState)
        {
            case EnemyState.Attack:
            p_chaseWaitTimer = chaseWaitTime;

            if (p_attackTimer < 0f)
            {
                StartCoroutine(AttackRoutine());
                p_attackTimer = stats.attackInterval;
            }
            break;

            case EnemyState.TargetInRange:
            p_chaseWaitTimer = chaseWaitTime;

            if (m_isAttacking)
            {
                if (canRotateWhileAttacking)
                {
                    RotateTowardsTarget();
                }
            }
            else if (!m_isAttacking)
            {
                RotateTowardsTarget();
            }
            break;

            case EnemyState.Chase:
            
            if (p_chaseWaitTimer < 0f)
            {
                p_movementControl.MoveTowardsPlayer();
            }
            break;

            default:
            break;
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
        p_agent.stoppingDistance = stats.attackRange;
        p_agent.autoBraking = stats.autoBraking;
    }

    private void CollisionDamageToPlayer(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player && p_contactDamageTimer <= 0)
        {
            player.GetHealth.TakeArmoredDamage(stats.contactDamage);
            p_contactDamageTimer = stats.attackInterval;
        }
    }

    protected bool m_isAttacking = false;

    protected IEnumerator AttackRoutine()
    {
        m_isAttacking = true;
        p_agent.enabled = false;
        p_agent.velocity = Vector3.zero;

        yield return new WaitForSeconds(waitBeforeAttackAnim);
        p_animController.AttackTrigger();
        p_attackTimer = stats.attackInterval;
        yield return new WaitForSeconds(waitAfterAttackAnim);

        m_isAttacking = false;
        p_agent.enabled = true;
    }

    public enum RotationDirection
    {
        NoRotation,
        Leftward,
        Rightward
    }


    private void RotateTowardsTarget()
    {
        Vector3 oldForward = transform.forward;

        Vector3 direction = p_player.transform.position - transform.position;
        Quaternion lookTo = Quaternion.LookRotation(direction);

        float rotateSpeed = rotateTowardsTargetSpeed * Time.deltaTime;
        Quaternion rotateTowards = Quaternion.RotateTowards(transform.rotation, lookTo, rotateSpeed * 10f);
        rotateTowards.x = 0f;

        transform.rotation = rotateTowards;

        Vector3 cross = Vector3.Cross(oldForward, transform.forward);

        if (cross.y > 0f)
        {
            m_rotationDirection = RotationDirection.Rightward;
        }
        else if (cross.y < 0f)
        {
            m_rotationDirection = RotationDirection.Leftward;
        }
        else
        {
            m_rotationDirection = RotationDirection.NoRotation;
        }
    }

    public abstract void StartAttackAnimationEvent();

    public abstract void StopAttackAnimationEvent();
    
    protected virtual void Die()
    {
        gameObject.SetActive(false);

        EnemySpawnManager.Instance.OnRemoveToEnemiesList(this);
        GameManager.Instance.UpdateKillCount();
        p_dropItemOnDestroy.DropItem();
    }

    // private void OnTakeDamage(int amount)
    // {
    //     GameManager.Instance.CreateDamagePopup(damagePopupSpawnTransform.position, amount);
    // }
}
