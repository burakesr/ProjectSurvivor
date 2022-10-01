using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private Collider m_collider;
    private int m_damage;

    private bool m_hasDamageInflicted = false;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    private void Start()
    {
        m_collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player && !m_hasDamageInflicted)
        {
            player.GetHealth.TakeArmoredDamage(m_damage);
            m_hasDamageInflicted = true;
        }
    }

    public void SetDamage(int damage)
    {
        m_damage = damage;
    }

    public void EnableTrigger()
    {
        m_collider.enabled = true;
        m_hasDamageInflicted = false;
    }

    public void DisableTrigger()
    {
        m_collider.enabled = false;
    }
}
