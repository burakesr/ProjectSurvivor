using System.Collections;
using UnityEngine;

public class RedMetalon : Enemy
{
    [Space(10)]
    [Header("PROJECTILE VARIABLES")]
    [SerializeField]
    private Transform projectileSpawnTransform;
    [SerializeField]
    private ProjectileTrajectory projectilePrefab;
    [SerializeField]
    private float speed;


    public override void StartAttackAnimationEvent()
    {
        ProjectileTrajectory projectile = PoolManager.Instance.SpawnFromPool(projectilePrefab.gameObject,
            projectileSpawnTransform.position, Quaternion.identity).GetComponent<ProjectileTrajectory>();

        projectile.SetUp(speed, stats.attackDamage);
        projectile.Fire(projectileSpawnTransform.position, m_player.transform.position);

        Vector3 predictedPos = HelperUtilities.GetPredictedPosition(m_player.transform.position, projectileSpawnTransform.position, 
            m_player.GetRigidbody.velocity, 
            projectile.Velocity.magnitude);

        projectile.Fire(projectile.transform.position, predictedPos);

        m_attackTimer = stats.attackInterval;

    }
}