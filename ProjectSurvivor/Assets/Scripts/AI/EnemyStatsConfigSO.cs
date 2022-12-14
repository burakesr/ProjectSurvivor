using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsConfig_New", menuName = "ScriptableObjects/Enemy/EnemyStatsConfig")]
public class EnemyStatsConfigSO : ScriptableObject
{
    [Header("GENERAL")]
    public string enemyName;
    public Sprite icon;

    [Space(10)]
    [Header("NAVIGATION AGENT VALUES")]
    public float speed = 1.5f;
    public float angularSpeed = 720f;
    public float acceleration = 50f;
    public float stoppingDistance = 0f;
    public bool autoBraking = true;

    [Space(10)]
    [Header("HEALTH")]
    public int maxHealth = 25;

    [Space(10)]
    [Header("DAMAGE")]
    public int contactDamage = 5;
    public float damageInterval = 1.5f;
}
