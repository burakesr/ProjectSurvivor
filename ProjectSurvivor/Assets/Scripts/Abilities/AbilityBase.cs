using System.Collections;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected float projectileSpawnInterval = 0.25f;

    public float timeToAttack = 1f;
    public int projectileCount = 1;

    protected AbilityDataSO p_weaponData;

    public AbilityStats GetWeaponStats
    {
        get
        {
            return m_weaponStats;
        }
        set
        {
            m_weaponStats = value;
        }
    }

    protected AbilityStats m_weaponStats;

    protected float p_timer;
    private int m_maxLevel;
    private int m_currentLevel;

    public int GetCurrentLevel => m_currentLevel;
    public int GetMaxLevel => m_maxLevel;
    protected WaitForSeconds p_projectileSpawnDelay;

    public abstract IEnumerator ProjectileSpawnRoutine();

    private void Start()
    {
        p_timer = timeToAttack;
        p_projectileSpawnDelay = new WaitForSeconds(projectileSpawnInterval);
    }

    private void Update()
    {
        p_timer -= Time.deltaTime;

        if (p_timer <= 0)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        p_timer = timeToAttack;
    }

    public virtual void SetData(AbilityDataSO weaponData)
    {
        p_weaponData = weaponData;

        m_weaponStats = new AbilityStats(weaponData.abilityStats.damage, weaponData.abilityStats.timeToAttack);
        timeToAttack = m_weaponStats.timeToAttack;

        m_maxLevel = weaponData.maxLevel;
    }

    public void LevelUp()
    {
        m_currentLevel++;
        m_currentLevel = Mathf.Clamp(m_currentLevel, 0, m_maxLevel);
        Debug.Log(gameObject.name + " Current Level: " + m_currentLevel);
    }
}
