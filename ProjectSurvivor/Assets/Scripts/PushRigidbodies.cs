using UnityEngine;
using UnityEngine.AI;

public class PushRigidbodies : MonoBehaviour
{
    [SerializeField] private float knockForce = 20f;

    private Rigidbody rb;
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        UndoKnockBack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            KnockBack(collision.transform.position);
        }
    }

    private void KnockBack(Vector3 targetPos)
    {
        rb.isKinematic = false;

        Vector3 knockDir = (targetPos - rb.position).normalized;
        rb.AddForce(knockDir * knockForce, ForceMode.Impulse);
    }

    private void UndoKnockBack()
    {
        while (rb.velocity.sqrMagnitude >= 0.1f)
        {
            agent.velocity = rb.velocity;
        }

        if (rb.velocity.sqrMagnitude <= 0.1f)
        {
            rb.isKinematic = true;
        }
    }
}