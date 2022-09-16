using UnityEngine;
using UnityEngine.AI;

public class KnockBackController : MonoBehaviour
{
    [SerializeField]
    private bool canBeKnocked = true;

    private Rigidbody rb;
    private NavMeshAgent agent;

    private bool isKnockBacked;

    private float deKnockBackTimer;

    private const float deKnockBackInterval = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        deKnockBackTimer -= Time.deltaTime;
        if (deKnockBackTimer <= 0f)
        {
            DeKnockBack();
        }
    }

    public void KnockBack(Transform knockFrom, float knockBackForce)
    {
        if (knockBackForce <= 0f || !canBeKnocked) return;

        isKnockBacked = true;
        deKnockBackTimer = deKnockBackInterval;

        rb.isKinematic = false;
        agent.enabled = false;

        Vector3 dir = (knockFrom.position - transform.position).normalized;

        rb.AddForce(dir * knockBackForce, ForceMode.Impulse);
    }

    private void DeKnockBack()
    {
        if (isKnockBacked)
        {
            float sqrMag = rb.velocity.magnitude * rb.velocity.magnitude;

            if (sqrMag <= 0.1f)
            {
                isKnockBacked = false;

                rb.isKinematic = true;
                agent.enabled = true;
            }
        }
    }
}