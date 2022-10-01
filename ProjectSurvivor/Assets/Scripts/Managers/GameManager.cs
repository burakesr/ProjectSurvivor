using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool isTestBuild = false;

    [Space(10)]
    [Header("REFERENCES")]
    [SerializeField]
    private Player playerPrefab;
    public DamagePopup damagePopupPrefab;
    [SerializeField]
    private GameObject cameras;

    [SerializeField]
    private float gameCountDownTime = 600;
    [SerializeField]
    private bool gameStarted = false;
    
    public float GetRemainingTime => gameCountDownTime;

    private int m_totalKilledEnemy;
    private Player m_player;
    private bool m_isGamePaused;

    [HideInInspector]
    public bool IsPlayerSpawned;
    public event Action OnPlayerSpawned;

    protected override void Awake()
    {
        base.Awake();

        //Application.targetFrameRate = 60;

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
        m_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        gameStarted = true;

        IsPlayerSpawned = true;
        OnPlayerSpawned?.Invoke();
    }

    public DamagePopup CreateDamagePopup(Vector3 position, int damageAmount, bool isCritical, bool isDamageOverTime)
    {
        DamagePopup popup = PoolManager.Instance.SpawnFromPool
            (damagePopupPrefab.gameObject,
            position,
            Quaternion.identity).GetComponent<DamagePopup>();

        popup.Setup(damageAmount, isCritical, isDamageOverTime);

        return popup;
    }


    public void UpdateKillCount()
    {
        m_totalKilledEnemy++;

        // Update kill count text
        UIManager.Instance.UpdateKillCountText();
    }

    public void PauseGame()
    {
        if (!m_isGamePaused)
        {
            Time.timeScale = 0.0f;
            m_isGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (m_isGamePaused)
        {
            Time.timeScale = 1.0f;
            m_isGamePaused = false;
        }
    }

    public int GetTotalKilledEnemyCount()
    {
        return m_totalKilledEnemy;
    }

    public Player GetPlayer()
    {
        return m_player;
    }

    public GameObject GetCameras()
    {
        return cameras;
    }
}
