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

    protected IDamageable p_damagedTarget;
    protected AbilityDataSO p_weaponData;

    protected int p_damage;
    protected float p_effectApplyCDTimer;
    protected float p_knockBackCDTimer;
    protected Vector3 p_moveDirection;
    protected bool p_hitDetected;

    public float Speed => speed;

    private void Update()
    {
        p_knockBackCDTimer -= Time.deltaTime;
    }

    public void SetUp(float speed, int damage)
    {
        this.speed = speed;
        p_damage = damage;
    }

    public void SetUp(float speed, Vector3 moveDirection, int damage, AbilityDataSO weaponData)
    {
        this.speed = speed;
        p_moveDirection = moveDirection;
        p_damage = damage;
        p_weaponData = weaponData;
    }


    public virtual void Fire() { }
    public virtual void Fire(Vector3 start, Vector3 end) { }

    protected void DamageAnEnemyOneTime(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            if (damageable != p_damagedTarget)
            {
                int damage = p_damage;
                damage = Damage(damage, ref isHitCritical);

                damageable.TakeDamage(damage, isHitCritical, false);
                p_damagedTarget = damageable;

                DamagePopup(other, damage);
                ApplyEffect(other);
                KnockBack(other);
            }
        }
    }

    protected void ApplyEffect(Collider other)
    {
        if (!p_weaponData.effect) return;
        
        int chanceToApplyEffect = Random.Range(0, 100);

        if (chanceToApplyEffect < p_weaponData.chanceToApplyEffect)
        {
            EffectManager target = other.GetComponent<EffectManager>();
            Effect effect = target.AddEffect(p_weaponData.effect);
            effect.SetTarget(other.GetComponent <Enemy>());
            effect.Enable();
        }
    }

    public void KnockBack(Collider collider)
    {
        if (p_knockBackCDTimer > 0f) return;

        KnockBackController knockBackController = collider.GetComponent<KnockBackController>();

        if (knockBackController)
        {
            knockBackController.KnockBack(transform, knockBackForce);
            p_knockBackCDTimer = knockBackCooldown;
        }
    }

    protected bool isHitCritical;
    
    protected int Damage(int damage, ref bool isHitCritical)
    {
        int totalDamage = damage;
        totalDamage = StatsManager.Instance.GetStrengthStat.DamageWithStrengthStat(totalDamage);
        
        isHitCritical = StatsManager.Instance.GetCriticalHitChanceStat.IsHitCritical();

        if (isHitCritical)
        {
            totalDamage = StatsManager.Instance.GetCriticalHitDamageStat.CriticalDamage(totalDamage);
        }

        return totalDamage;
    }

    protected void DamagePopup(Collider other, int damage){
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy){
            //Create damage popup
            GameManager.Instance.CreateDamagePopup(enemy.GetDamagePopupSpawnTransform.position, 
            damage, isHitCritical, false);
        }
    }

    protected void DisableProjectile()
    {
        gameObject.SetActive(false);
        p_damagedTarget = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (hitDetectTransform != null)
            Gizmos.DrawWireSphere(hitDetectTransform.position, hitDetectRadius);
    }
}
