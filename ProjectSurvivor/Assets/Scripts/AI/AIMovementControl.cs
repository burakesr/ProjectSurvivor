using UnityEngine;
using UnityEngine.AI;

public class AIMovementControl : MonoBehaviour
{
    [SerializeField] 
    private float pathInterval = 0.3f;

    public Vector3 GetCurrentVelocity { get; private set; }

    private NavMeshAgent m_agent;
    private KnockBackController m_knockBackController;
    private Player m_player;

    private float m_pathFindTimer;
    private bool m_isKnockBacked = false;

    private Vector3 m_targetPosition;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_knockBackController = GetComponent<KnockBackController>();
    }

    private void Start()
    {
        m_player = GameManager.Instance.GetPlayer();
    }

    private void Update()
    {
        m_pathFindTimer -= Time.deltaTime;

        GetCurrentVelocity = m_agent.velocity;
    }

    public void MoveTowardsPlayer()
    {
        if (m_player == null) return;
        if (m_pathFindTimer > 0f) return;
        if (m_targetPosition == m_player.transform.position) return;

        Vector3 playerPos = m_player.transform.position;

        //Vector3 targetVelocity = m_player.GetRigidbody.velocity;
        //Vector3 predictedPos = HelperUtilities.GetPredictedPosition(m_player.transform.position, transform.position, targetVelocity, m_agent.velocity.magnitude);

        if (m_player && m_agent.enabled)
        {
            m_targetPosition = playerPos;
            m_agent.SetDestination(m_targetPosition);
            m_pathFindTimer = pathInterval;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() && !m_isKnockBacked)
        {
            m_knockBackController.KnockBack(collision.transform, 1f);
        }
    }
}
