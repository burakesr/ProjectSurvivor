using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : SingletonMonoBehaviour<EnemySpawnManager>
{
    [SerializeField] 
    private SpawnableEnemy[] spawnableEnemies;
    [SerializeField] 
    private Vector3 spawnArea;
    [SerializeField] 
    private AnimationCurve spawnTimeCurve;
    [SerializeField]
    private AnimationCurve maxAliveEnemyCountByTimeCurve;
    [SerializeField]
    private float bossSpawnTime = 15f;

    private float basicEnemySpawnTimer;
    private float bossEnemySpawnTimer;

    private int aliveEnemyCount;

    private List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {        
        bossEnemySpawnTimer = bossSpawnTime;
    }


    private void Update()
    {
        //if (GameManager.Instance.isTestBuild) return;
        if (!GameManager.Instance.IsPlayerSpawned) return;

        basicEnemySpawnTimer -= Time.deltaTime;
        bossEnemySpawnTimer -= Time.deltaTime;

        if (basicEnemySpawnTimer <= 0f && IsAliveEnemyCountLessThenMax())
        {
            SpawnBasicEnemy();
            basicEnemySpawnTimer = spawnTimeCurve.Evaluate(GameManager.Instance.GetRemainingTime);
        }

        if (bossEnemySpawnTimer <= 0f && IsAliveEnemyCountLessThenMax())
        {
            SpawnBossEnemy();
            bossEnemySpawnTimer = bossSpawnTime;
        }
    }

    private bool IsAliveEnemyCountLessThenMax()
    {
        return aliveEnemyCount < (int)maxAliveEnemyCountByTimeCurve.Evaluate(GameManager.Instance.GetRemainingTime);
    }

    private void SpawnBasicEnemy()
    {
        Vector3 position = SpawnPosition();

        EnemyToSpawn(position, EnemyType.Basic);
    }

    private void SpawnBossEnemy()
    {
        Vector3 position = SpawnPosition();

        EnemyToSpawn(position, EnemyType.Boss);
    }

    private Vector3 SpawnPosition()
    {
        Vector3 position = new Vector3();

        float value = Random.value > 0.5f ? -1f : 1f;
        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.z = spawnArea.z * value;
        }
        else
        {
            position.z = Random.Range(-spawnArea.z, spawnArea.z);
            position.x = spawnArea.x * value;
        }

        position.y = 0f;

        position += GameManager.Instance.GetPlayer().transform.position;
        return position;
    }

    public GameObject EnemyToSpawn(Vector3 position, EnemyType enemyType)
    {
        float bestRatio = Mathf.NegativeInfinity;
        Enemy enemy = null;

        for (int i = 0; i < spawnableEnemies.Length; i++)
        {
            if (spawnableEnemies[i].type == enemyType)
            {
                float ratio = Random.Range(0f, 1f) * spawnableEnemies[i].spawnOverTimeRatio.Evaluate(GameManager.Instance.GetRemainingTime);
            
                if (ratio > bestRatio)
                {
                    bestRatio = ratio;
                    enemy = spawnableEnemies[i].prefab;
                }
            }
        }

        PoolManager.Instance.SpawnFromPool(enemy.gameObject, position, Quaternion.identity);
        
        OnAddToEnemiesList(enemy);

        return enemy.gameObject;
    }

    public void OnAddToEnemiesList(Enemy enemy)
    {
        aliveEnemyCount++;
        enemies.Add(enemy);
    }

    public void OnRemoveToEnemiesList(Enemy enemy)
    {
        aliveEnemyCount--;
        enemies.Remove(enemy);
    }
}

[System.Serializable]
public class SpawnableEnemy
{
    public EnemyType type;
    public Enemy prefab;
    public AnimationCurve spawnOverTimeRatio;
}

public enum EnemyType
{
    Basic,
    Boss,
    Pack
}
