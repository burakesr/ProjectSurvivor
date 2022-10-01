using System.Collections;
using UnityEngine;

public class MeteorRain : AbilityBase
{
    [SerializeField] 
    private ProjectileBase meteorPrefab;
    [SerializeField]
    private float spawnHeight = 20f;
    [SerializeField]
    private Vector3 randomTargetPoint;

    public override void Attack()
    {
        base.Attack();

        StartCoroutine(ProjectileSpawnRoutine());
    }


    public override IEnumerator ProjectileSpawnRoutine()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(projectileSpawnInterval);
        }
    }

    private void SpawnProjectile()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, spawnHeight, 0f);

        ProjectileBase meteor = PoolManager.Instance.SpawnFromPool(meteorPrefab.gameObject, spawnPos, Quaternion.identity).GetComponent<ProjectileBase>();
        meteor.SetUp(meteorPrefab.Speed, Vector3.zero, m_weaponStats.damage, p_weaponData);

        Vector3 targetPos = transform.position + new Vector3(Random.Range(randomTargetPoint.x, -randomTargetPoint.x),
            0f, Random.Range(randomTargetPoint.z, -randomTargetPoint.z));

        targetPos.y = 0f;

        meteor.Fire(spawnPos, targetPos);
    }
}