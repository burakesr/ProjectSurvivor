using UnityEngine;

public class StoneHover : Enemy
{
    [Space(10)]
    [Header("PROJECTILE VARIABLES")]
    [SerializeField]
    private Transform[] projectileSpawnTransform;
    [SerializeField]
    private ProjectileTrajectory projectilePrefab;
    [SerializeField]
    private float speed;

    public override void StartAttackAnimationEvent()
    {
        Transform spawnPos;
        if (projectileSpawnTransform.Length > 1)
        {
            spawnPos = projectileSpawnTransform[Random.Range(0, projectileSpawnTransform.Length)];
        }
        else
        {
            spawnPos = projectileSpawnTransform[0];
        }

        ProjectileTrajectory projectile = PoolManager.Instance.SpawnFromPool(projectilePrefab.gameObject,
            spawnPos.position, Quaternion.identity).GetComponent<ProjectileTrajectory>();

        projectile.SetUp(speed, stats.attackDamage);
        projectile.Fire(spawnPos.position, p_player.transform.position);

        Vector3 predictedPos = HelperUtilities.GetPredictedPosition(p_player.transform.position, spawnPos.position, 
            p_player.GetRigidbody.velocity, 
            projectile.Velocity.magnitude);

        projectile.Fire(projectile.transform.position, predictedPos);

        p_attackTimer = stats.attackInterval;
    }

    public override void StopAttackAnimationEvent()
    {
    }
}
