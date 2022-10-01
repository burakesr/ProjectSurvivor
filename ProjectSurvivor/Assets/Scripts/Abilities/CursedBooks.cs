using System.Collections;
using UnityEngine;

public class CursedBooks : AbilityBase
{
    [SerializeField]
    private ProjectileBase bookPrefab;
    [SerializeField]
    private float bookSpawnRadius = 5f;
    [SerializeField]
    private float spawnPosY = 1f;

    public override void Attack()
    {
        base.Attack();

        for (int i = 0; i < projectileCount; i++)
        {
            /* Distance around the circle */
            float radians = 2 * Mathf.PI / projectileCount * i;

            /* Get the vector direction */
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);

            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            Vector3 spawnPos = transform.position + (spawnDir * bookSpawnRadius); // Radius is just the distance away from the point
            spawnPos.y = transform.position.y + spawnPosY;

            /* Now spawn */
            ProjectileBase bookInstance = Instantiate(bookPrefab, spawnPos, Quaternion.identity);
            //PoolManager.Instance.SpawnFromPool(bookPrefab.gameObject, spawnPos, Quaternion.identity).GetComponent<ProjectileBase>();
            
            bookInstance.SetUp(bookInstance.Speed, Vector3.zero, m_weaponStats.damage, p_weaponData);
        }
    }

    public override IEnumerator ProjectileSpawnRoutine()
    {
        yield return null;
    }
}
