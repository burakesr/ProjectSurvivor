using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool isTestBuild = false;

    [Space(10)]
    [Header("REFERENCES")]
    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private DamagePopup damagePopupPrefab;
    [SerializeField]
    private GameObject cameras;

    [SerializeField]
    private float gameCountDownTime = 600;
    [SerializeField]
    private bool gameStarted = false;
    
    public float GetRemainingTime => gameCountDownTime;

    private int totalKilledEnemy;
    private Player player;

    public bool IsPlayerSpawned;
    public event Action OnPlayerSpawned;

    protected override void Awake()
    {
        base.Awake();

        if (isTestBuild)
        {
            InitialisePlayer();
        }
    }

    private void Update()
    {
        if (!gameStarted) return;

        if (gameCountDownTime > 0)
        {
            gameCountDownTime -= Time.deltaTime;
            DisplayTime(gameCountDownTime);
        }
        else
        {
            Debug.Log("Time has run out!");
            gameCountDownTime = 0;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        UIManager.Instance.UpdateTimerText(minutes, seconds);
    }

    public void InitialisePlayer()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        gameStarted = true;

        IsPlayerSpawned = true;
        OnPlayerSpawned?.Invoke();
    }

    public DamagePopup CreateDamagePopup(Vector3 position, int damageAmount)
    {
        DamagePopup popup = PoolManager.Instance.SpawnFromPool
            (damagePopupPrefab.gameObject,
            position,
            Quaternion.identity).GetComponent<DamagePopup>();

        popup.Setup(damageAmount);

        return popup;
    }


    public void UpdateKillCount()
    {
        totalKilledEnemy++;

        // Update kill count text
        UIManager.Instance.UpdateKillCountText();
    }

    public int GetTotalKilledEnemyCount()
    {
        return totalKilledEnemy;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public GameObject GetCameras()
    {
        return cameras;
    }
}
