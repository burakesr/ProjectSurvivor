using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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
    protected EnemyState m_currentState;

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

    public Transform GetAimPoint => aimPoint;
    public Health GetHealth => m_health;
    public NavMeshAgent GetAgent => m_agent;
    public AIMovementControl GetMovementControl => m_movementControl;
    public Player GetPlayer => m_player;

    protected Health m_health;
    protected DropItemOnDestroy m_dropItemOnDestroy;
    protected NavMeshAgent m_agent;
    protected Player m_player;
    protected AIMovementControl m_movementControl;
    protected AnimatorController m_animController;

    protected float m_contactDamageTimer;
    protected float m_attackTimer;
    protected float m_agentStopDelayTimer;

    protected const float m_agentStopDelay = 0.5f;

    private void Awake()
    {
        m_health = GetComponent<Health>();
        m_agent = GetComponent<NavMeshAgent>();
        m_dropItemOnDestroy = GetComponent<DropItemOnDestroy>();
        m_movementControl = GetComponent<AIMovementControl>();
        m_animController = GetComponent<AnimatorController>();
    }

    private void Start()
    {
        StatsInitialisation();

        m_player = GameManager.Instance.GetPlayer();
    }

    private void OnEnable()
    {
        m_health.OnTakeDamage += OnTakeDamage;
        m_health.OnDie += Die;

        m_health.SetStartingHealth(stats.maxHealth);
    }

    private void OnDisable()
    {
        m_health.OnTakeDamage -= OnTakeDamage;
        m_health.OnDie -= Die;
    }

    private void Update()
    {
        m_contactDamageTimer -= Time.deltaTime;
        m_attackTimer -= Time.deltaTime;
        m_agentStopDelayTimer -= Time.deltaTime;

        bool targetInRangeOfAttack = Vector3.Distance(transform.position, m_player.transform.position) < stats.attackRange;

        AIStateHandler(targetInRangeOfAttack);
        AnimatonHandler();
    }

    private void AnimatonHandler()
    {
        if (m_agent.velocity.sqrMagnitude >= 0.1f)
        {
            m_animController.OnCharacterWalking();
        }
        else if (m_agent.velocity.sqrMagnitude <= 0.1f)
        {
            m_animController.OnCharacterIdle();
        }
    }

    private void AIStateHandler(bool targetInRangeOfAttack)
    {
        if (targetInRangeOfAttack && m_attackTimer < 0f)
        {
            m_currentState = EnemyState.Attack;
        }
        else if (targetInRangeOfAttack && m_attackTimer > 0f)
        {
            m_currentState = EnemyState.TargetInRange;
        }
        else if (!targetInRangeOfAttack)
        {
            m_currentState = EnemyState.Chase;
        }

        switch (m_currentState)
        {
            case EnemyState.Attack:
                if (m_attackTimer < 0f)
                {
                    StartCoroutine(AttackRoutine());
                }
                break;

            case EnemyState.TargetInRange:
                RotateTowardsTarget();
                break;

            case EnemyState.Chase:
                m_movementControl.MoveTowardsPlayer();
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
        m_agent.speed = stats.speed;
        m_agent.angularSpeed = stats.angularSpeed;
        m_agent.acceleration = stats.acceleration;
        m_agent.stoppingDistance = stats.attackRange;
        m_agent.autoBraking = stats.autoBraking;
    }

    private void CollisionDamageToPlayer(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player && m_contactDamageTimer <= 0)
        {
            player.GetHealth.TakeArmoredDamage(stats.contactDamage);
            m_contactDamageTimer = stats.attackInterval;
        }
    }

    protected bool m_isAttacking = false;

    protected IEnumerator AttackRoutine()
    {
        m_agent.enabled = false;
        m_isAttacking = true;
        m_agent.velocity = Vector3.zero;

        yield return new WaitForSeconds(waitBeforeAttackAnim);
        m_animController.AttackTrigger();
        m_attackTimer = stats.attackInterval;
        yield return new WaitForSeconds(waitAfterAttackAnim);

        m_isAttacking = false;
        m_agent.enabled = true;
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

        Vector3 direction = m_player.transform.position - transform.position;
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

    public virtual void StartAttackAnimationEvent()
    {
        attackTrigger.SetDamage(stats.attackDamage);
        attackTrigger.EnableTrigger();
    }

    public virtual void StopAttackAnimationEvent()
    {
        attackTrigger.DisableTrigger();
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);

        EnemySpawnManager.Instance.OnRemoveToEnemiesList(this);
        GameManager.Instance.UpdateKillCount();
        m_dropItemOnDestroy.DropItem();
    }

    private void OnTakeDamage(int amount)
    {
        GameManager.Instance.CreateDamagePopup(damagePopupSpawnTransform.position, amount);
    }
}
