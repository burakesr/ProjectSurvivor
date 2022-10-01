using System.Collections;
using UnityEngine;

public class ThrowTrajectoryProjectileWeapon : AbilityBase
{
    [SerializeField] 
    private ProjectileBase projectilePrefab;

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(ProjectileSpawnRoutine());

    }

    public override IEnumerator ProjectileSpawnRoutine()
    {
        Transform[] targets = GameManager.Instance.GetPlayer().GetNearestEnemyToThePlayer.GetNearestEnemies(projectileCount);

        for (int i = 0; i < projectileCount; i++)
        {
            ProjectileTrajectory projectile = PoolManager.Instance.SpawnFromPool
            (projectilePrefab.gameObject,
            transform.position,
            Quaternion.identity).GetComponent<ProjectileTrajectory>();

            Vector3 targetVelocity;
            Vector3 targetPos = new Vector3(transform.position.x + Random.Range(2f, -2f), 0f, transform.position.z + Random.Range(2f, -2f));
            Vector3 predictedTargetPos = targetPos;

            projectile.SetUp(projectile.Speed, transform.position, m_weaponStats.damage, p_weaponData);
            projectile.Fire(transform.position, targetPos);

            if (targets[i] != null)
            {
                AIMovementControl aIMovementControl = targets[i].GetComponentInParent<AIMovementControl>();
                targetVelocity = aIMovementControl ? aIMovementControl.GetCurrentVelocity : new Vector3(.5f, 0f, -.5f);
                predictedTargetPos = HelperUtilities.GetPredictedPosition(targets[i].position, transform.position, targetVelocity, projectile.Velocity.magnitude);
            }
            
            if (predictedTargetPos != targetPos)
            {
                projectile.Fire(projectile.transform.position, predictedTargetPos);
            }

            Quaternion lookRotation = Quaternion.LookRotation(transform.position - predictedTargetPos);
            lookRotation.x = 0;
            lookRotation.z = 0;

            projectile.transform.rotation = lookRotation;

            yield return p_projectileSpawnDelay;
        }
    }
}
