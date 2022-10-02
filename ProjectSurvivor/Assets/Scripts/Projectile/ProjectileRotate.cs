using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotate : ProjectileBase
{
    [SerializeField]
    private bool rotateAroundPlayer = false;
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private float damageCooldownTime = 0.25f;
    [SerializeField]
    private float collectiveEffectApplyTime = 0.5f;
    [SerializeField]
    private bool destroyOnLifeTimeEnd = false;

    private Transform pivotPoint;
    private Vector3 offSetDirection;
    private float distance;

    private float lifeTimer;
    private float damageCooldownTimer;
    private float collectiveEffectApplyTimer;

    private List<Collider> collectiveApplyEffectTargets = new List<Collider>();

    private void Start()
    {
        if (rotateAroundPlayer)
        {
            SetPivot(GameManager.Instance.GetPlayer().transform);
        }
        else
        {
            SetPivot(transform);
        }
    }

    private void OnEnable()
    {
        lifeTimer = lifeTime;
    }

    private void OnDisable() 
    {
        transform.position = Vector3.zero;
    }


    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        damageCooldownTimer -= Time.deltaTime;
        collectiveEffectApplyTimer -= Time.deltaTime;
        
        if (rotateAroundPlayer)
        {
            RotateAroundObject();
        }
        else
        {
            RotateAroundItself();
        }

        if (lifeTimer <= 0f)
        {
            if (destroyOnLifeTimeEnd)
            {
                Destroy(gameObject);
            }
            else
            {
                DisableProjectile();
            }
        }

        if (collectiveApplyEffectTargets.Count > 0 && collectiveEffectApplyTimer >= collectiveEffectApplyTime)
        {
            for (int i = 0; i < collectiveApplyEffectTargets.Count; i++)
            {
                ApplyEffect(collectiveApplyEffectTargets[i]);
            }
            for (int i = 0; i < collectiveApplyEffectTargets.Count; i++)
            {
                collectiveApplyEffectTargets.Remove(collectiveApplyEffectTargets[i]);
            }

            collectiveEffectApplyTimer = p_weaponData.effectApplyCooldown;
        }
    }

    private void RotateAroundObject()
    {
        Quaternion rotate = Quaternion.Euler(0, speed * 90f * Time.deltaTime , 0);
        offSetDirection = (rotate * offSetDirection).normalized;
        transform.position = pivotPoint.position + offSetDirection * distance;
    }

    private void RotateAroundItself()
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, speed * 90f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            if (damageable != p_damagedTarget)
            {
                p_damagedTarget = damageable;
                InflictDamage(other, damageable);
            }
            else
            {
                if (damageCooldownTimer <= 0f)
                {
                    InflictDamage(other, damageable);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (rotateAroundPlayer) return;
        if (damageCooldownTimer >= 0f) return;

        Collider[] colliders = Physics.OverlapSphere(hitDetectTransform.position, hitDetectRadius, hitLayer, QueryTriggerInteraction.Ignore);
        foreach (var collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                InflictDamage(collider, damageable);
            }
        }
    }

    private void InflictDamage(Collider collider, IDamageable target)
    {
        // Add colliders to collective effect apply list
        if (collectiveEffectApplyTimer > 0f)
        {
            if (collectiveApplyEffectTargets.Contains(collider)) return;

            collectiveApplyEffectTargets.Add(collider);
        }

        int damage = p_damage;
        damage = Damage(damage, ref isHitCritical);

        target.TakeDamage(damage, isHitCritical, false);

        //Damage popup
        DamagePopup(collider, damage);
        //ApplyEffect(other);
        KnockBack(collider);

        damageCooldownTimer = damageCooldownTime;
    }

    public void SetPivot(Transform pivot)
    {
        if (pivot != null)
        {
            pivotPoint = pivot;
            offSetDirection = transform.position - pivotPoint.position;
            distance = offSetDirection.magnitude;
        }
        else
        {
            pivotPoint = null;
        }
    }
}
