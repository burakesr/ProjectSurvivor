using System.Collections.Generic;
using UnityEngine;

public class ProjectileFromAbove : ProjectileBase
{
    [SerializeField]
    private float radius = 2f;

    private List<Collider> damagedTargets = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, hitLayer, QueryTriggerInteraction.Ignore);

        foreach (Collider collider in colliders)
        {
            if (!damagedTargets.Contains(collider))
            {
                DamageAnEnemyOneTime(collider);
                damagedTargets.Add(collider);
            }
        }
    }

    private void Update()
    {
        if (transform.position.y <= 0f)
        {
            for (int i = 0; i < damagedTargets.Count; i++)
            {
                damagedTargets.RemoveAt(i);
            }

            gameObject.SetActive(false);
        }

        transform.position += p_moveDirection * speed * Time.deltaTime;

    }

    public override void Fire(Vector3 start, Vector3 end)
    {
        p_moveDirection = (end - start).normalized;
    }
}
