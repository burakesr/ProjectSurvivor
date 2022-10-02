using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    private int maxHealth;
    private int currentHealth;
    private bool isDead;

    public int GetMaxHealth => maxHealth;
    public int GetCurrentHealth => currentHealth;

    public UnityAction<int, bool, bool> OnTakeDamage;
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

    public void TakeDamage(int damageAmount, bool isCritical, bool isDamageOverTime)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        OnTakeDamage?.Invoke(damageAmount, isCritical, isDamageOverTime);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void TakeArmoredDamage(int damageAmount)
    {
        if (isDead) return;

        ArmorStat armorStat = StatsManager.Instance.GetArmorStat;

        if (armorStat != null)
        {
            damageAmount = armorStat.ArmoredDamage(damageAmount);
        }

        currentHealth -= damageAmount;

        OnTakeDamage?.Invoke(damageAmount, false, false);

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
}
