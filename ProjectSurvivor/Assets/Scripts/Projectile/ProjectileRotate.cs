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

    private Transform pivotPoint;
    private Vector3 offSetDirection;
    private float distance;

    private float lifeTimer;
    private float damageCooldownTimer;
    private float collectiveEffectApplyTimer;

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
            DisableProjectile();
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

            collectiveEffectApplyTimer = weaponData.effectApplyCooldown;
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

    private List<Collider> collectiveApplyEffectTargets = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health && !health.isPlayer)
        {
            if (health != damagedEnemy)
            {
                damagedEnemy = health;

                InflictDamage(other, health);
            }

            if (health == damagedEnemy)
            {
                if (damageCooldownTimer <= 0f)
                {
                    InflictDamage(other, health);
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
            Health health = collider.GetComponent<Health>();
            if (health && !health.isPlayer)
            {
                InflictDamage(collider, health);
            }
        }
    }

    private void InflictDamage(Collider other, Health health)
    {
        // Add colliders to collective effect apply list
        if (collectiveEffectApplyTimer > 0f)
        {
            if (collectiveApplyEffectTargets.Contains(other)) return;

            collectiveApplyEffectTargets.Add(other);
        }

        health.TakeDamage(damage);
        //ApplyEffect(other);
        KnockBack(other);

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
