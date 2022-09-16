using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GetNearestEnemyToThePlayer : MonoBehaviour
{
    [SerializeField] 
    private float radius;
    [SerializeField] 
    private LayerMask enemyLayer;
    [SerializeField]
    private int test;
    [SerializeField]
    private Target[] targets;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            GetNearestEnemies(test);
        }
    }

    public Transform[] GetNearestEnemies(int count)
    {
        Collider[] enemies = new Collider[10];
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (enemiesInRange.Length == 0) return new Transform[count];
        
        int validTargetCount = 0;
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            if (i > enemies.Length - 1) break;

            enemies[i] = enemiesInRange[i];
            validTargetCount++;
        }

        targets = new Target[validTargetCount];

        for (int i = 0; i < targets.Length; i++)
        {
            if (enemies[i].transform == null) break;

            float distanceToTarget = Vector3.Distance(transform.position, enemies[i].transform.position);
            targets[i] = new Target(enemies[i].transform, distanceToTarget);
        }

        // sorting by distance
        for (int i = 1; i < targets.Length; i++)
        {
            Target temp = targets[i];
            int j;

            for (j = i - 1; j >= 0 && targets[j].distance > temp.distance; j--)
            {
                targets[j + 1] = targets[j];
            }
            targets[j + 1] = temp;
        }

        Transform[] closestTransforms = new Transform[count];
        for (int i = 0; i < targets.Length; i++)
        {
            if (i >= closestTransforms.Length) break;

            if (targets[i].transform != null)
                closestTransforms[i] = targets[i].transform.GetComponent<Enemy>().GetAimPoint;
            else
                closestTransforms[i] = null;
        }

        return closestTransforms;
    }
}

[Serializable]
public struct Target
{
    public Transform transform;
    public float distance;

    public Target(Transform transform, float distance)
    {
        this.transform = transform;
        this.distance = distance;
    }
};
