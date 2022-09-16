using System.Collections;
using UnityEngine;

public class CursedBooks : WeaponBase
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
            var radians = 2 * Mathf.PI / projectileCount * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = transform.position + spawnDir * bookSpawnRadius; // Radius is just the distance away from the point
            spawnPos.y = transform.position.y + spawnPosY;

            /* Now spawn */
            ProjectileBase bookInstance = PoolManager.Instance.SpawnFromPool(bookPrefab.gameObject, spawnPos, Quaternion.identity).GetComponent<ProjectileBase>();

            bookInstance.SetUp(bookInstance.Speed, Vector3.zero, weaponStats.damage, weaponData);
        }

    }

    public override IEnumerator ProjectileSpawnRoutine()
    {
        yield return null;
    }
}
