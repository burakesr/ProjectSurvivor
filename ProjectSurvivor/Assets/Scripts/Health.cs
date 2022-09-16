using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public bool isPlayer = false;

    private int maxHealth;
    private int currentHealth;
    private bool isDead;

    public int GetMaxHealth => maxHealth;
    public int GetCurrentHealth => currentHealth;

    public UnityAction<int> OnTakeDamage;
    public UnityAction<int> OnHeal;
    public UnityAction OnDie;

    private void OnEnable()
    {
        isDead = false;
    }

    public void SetStartingHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        damageAmount = DamageWithStrengthStat(damageAmount);

        currentHealth -= damageAmount;

        OnTakeDamage?.Invoke(damageAmount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void TakeArmoredDamage(int damageAmount)
    {
        if (isDead) return;

        ArmorStat armorStat = GameManager.Instance.GetPlayer().GetStatsManager.GetArmorStat;

        if (armorStat != null)
        {
            damageAmount = armorStat.ArmoredDamage(damageAmount);
        }

        currentHealth -= damageAmount;

        OnTakeDamage?.Invoke(damageAmount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        int newHealth = currentHealth + healAmount;
        currentHealth = Mathf.Clamp(newHealth, currentHealth, maxHealth);

        OnHeal?.Invoke(healAmount);
    }

    public void Die()
    {
        isDead = true;
        OnDie?.Invoke();
    }

    public float GetHealthFraction()
    {
        return (float)currentHealth / maxHealth;
    }

    private int DamageWithStrengthStat(int damageAmount)
    {
        damageAmount += GameManager.Instance.GetPlayer().GetStatsManager.GetStrengthStat.value;
        return damageAmount;
    }
}
