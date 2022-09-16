using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [Space(10)]
    [Header("KNOCKBACK")]
    [SerializeField]
    private float knockBackCooldown = 1f;
    [SerializeField]
    protected float knockBackForce = 2f;
    [Space(10)]
    [Header("HIT DETECTION")]
    [SerializeField]
    protected LayerMask hitLayer;
    [SerializeField]
    protected Transform hitDetectTransform = null;
    [SerializeField]
    protected float hitDetectRadius = 0.4f;

    protected Health damagedEnemy;
    protected WeaponDataSO weaponData;

    protected int damage;
    protected float effectApplyCDTimer;
    protected float knockBackCDTimer;
    protected Vector3 moveDirection;
    protected bool hitDetected;

    public float Speed => speed;

    private void Update()
    {
        knockBackCDTimer -= Time.deltaTime;
    }

    public void SetUp(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public void SetUp(float speed, Vector3 moveDirection, int damage, WeaponDataSO weaponData)
    {
        this.speed = speed;
        this.moveDirection = moveDirection;
        this.damage = damage;
        this.weaponData = weaponData;
    }


    public virtual void Fire() { }
    public virtual void Fire(Vector3 start, Vector3 end) { }

    protected void DamageAnEnemyOneTime(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health && !health.isPlayer)
        {
            if (health != damagedEnemy)
            {
                health.TakeDamage(damage);
                damagedEnemy = health;
                
                ApplyEffect(other);
                KnockBack(other);
            }
        }
    }

    protected void ApplyEffect(Collider other)
    {
        if (!weaponData.effect) return;
        
        int chanceToApplyEffect = Random.Range(0, 100);

        if (chanceToApplyEffect < weaponData.chanceToApplyEffect)
        {
            EffectManager target = other.GetComponent<EffectManager>();
            Effect effect = target.AddEffect(weaponData.effect);
            effect.SetTarget(other.GetComponent <Enemy>());
            effect.Enable();
        }
    }

    public void KnockBack(Collider collider)
    {
        if (knockBackCDTimer > 0f) return;

        KnockBackController knockBackController = collider.GetComponent<KnockBackController>();

        if (knockBackController)
        {
            knockBackController.KnockBack(transform, knockBackForce);
            knockBackCDTimer = knockBackCooldown;
        }
    }

    protected void DisableProjectile()
    {
        gameObject.SetActive(false);
        damagedEnemy = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (hitDetectTransform != null)
            Gizmos.DrawWireSphere(hitDetectTransform.position, hitDetectRadius);
    }
}
