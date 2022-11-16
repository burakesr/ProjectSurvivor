using System.Collections;
using UnityEngine;

public class ThrowingDagger : AbilityBase
{
    [SerializeField] 
    private ProjectileBase daggerPrefab;


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
            ProjectileBase projectile = PoolManager.Instance.SpawnFromPool
            (daggerPrefab.gameObject,
            transform.position,
            Quaternion.identity).GetComponent<ProjectileBase>();

            projectile.SetUp(projectile.Speed, Vector3.zero, m_weaponStats.damage, p_weaponData);
            
            if (targets[i] != null)
            {
                projectile.Fire(transform.position, targets[i].position);
                projectile.transform.rotation = Quaternion.Euler(0f, projectile.transform.eulerAngles.y, 0);
            }
            else
            {
                Vector3 direction = new Vector3(1f, 0.5f, 1f);
                Vector3 randomDirection = new Vector3(Random.Range(direction.x, -direction.x), direction.y, Random.Range(direction.z, -direction.z));
                projectile.Fire(transform.position, randomDirection);
            }

            yield return p_projectileSpawnDelay;
        }
    }
}
