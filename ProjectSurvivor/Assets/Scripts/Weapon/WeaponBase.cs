using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField]
    protected float projectileSpawnInterval = 0.25f;

    public float timeToAttack = 1f;
    public int projectileCount = 1;

    protected WeaponDataSO weaponData;

    public WeaponStats GetWeaponStats
    {
        get
        {
            return weaponStats;
        }
        set
        {
            weaponStats = value;
        }
    }

    protected WeaponStats weaponStats;

    protected float timer;
    private int maxLevel;
    private int currentLevel;

    public int GetCurrentLevel => currentLevel;
    public abstract IEnumerator ProjectileSpawnRoutine();

    private void Start()
    {
        timer = timeToAttack;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        timer = timeToAttack;
    }

    public virtual void SetData(WeaponDataSO weaponData)
    {
        this.weaponData = weaponData;

        weaponStats = new WeaponStats(weaponData.weaponStats.damage, weaponData.weaponStats.timeToAttack);
        timeToAttack = weaponStats.timeToAttack;

        maxLevel = weaponData.maxLevel;
    }

    public void LevelUp()
    {
        currentLevel++;
        currentLevel = Mathf.Clamp(currentLevel, 0, maxLevel);
    }
}
