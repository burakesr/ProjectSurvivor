using System.Collections;
using UnityEngine;

public class RotatingSwords : AbilityBase
{
    [SerializeField]
    private ProjectileBase swordPrefab;
    [SerializeField]
    private float spawnX, spawnZ;

    private const float spawnY = 1.5f;

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(ProjectileSpawnRoutine());
    }

    public override IEnumerator ProjectileSpawnRoutine()
    {
        Transform[] targets = GameManager.Instance.GetPlayer().GetNearestEnemyToThePlayer.GetNearestEnemies(projectileCount);
        Vector3 swordSpawnPos;

        for (int i = 0; i < projectileCount; i++)
        {
            if (targets[i] != null)
            {
                swordSpawnPos = RandomSpawnPosition(targets[i]);
            }
            else
            {
                swordSpawnPos = RandomSpawnPosition(transform);
            }

            ProjectileBase swordInstance = PoolManager.Instance.SpawnFromPool
            (swordPrefab.gameObject, swordSpawnPos, Quaternion.Euler(90f, 0f, 0f)).GetComponent<ProjectileBase>();

            swordInstance.SetUp(swordInstance.Speed, Vector3.zero, m_weaponStats.damage, p_weaponData);

            yield return p_projectileSpawnDelay;
        }
    }

    private Vector3 RandomSpawnPosition(Transform target)
    {
        return new Vector3(Random.Range(target.position.x + spawnX, target.position.x - spawnX),
            spawnY,
            Random.Range(target.position.z + spawnZ, target.position.z - spawnZ));
    }
}