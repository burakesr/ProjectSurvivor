using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Header("REFERENCES")]
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject gameplayUI;
    
    [Space(10)]
    [Header("GAMEPLAY UI")]
    [SerializeField]
    private TextMeshProUGUI killCountText;
    [SerializeField]
    private TextMeshProUGUI timeText;

    [Header("LOADING SCENE")]
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Image loadingBar;

    protected override void Awake()
    {
        base.Awake();
    }

    public void UpdateKillCountText()
    {
        killCountText.text = GameManager.Instance.GetTotalKilledEnemyCount().ToString();
    }

    public void UpdateTimerText(float minutes, float seconds)
    {
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OpenGameplayUI()
    {
        gameplayUI.SetActive(true);
    }

    public void CloseGameplayUI()
    {
        gameplayUI.SetActive(false);
    }

    public void OpenMainMenu()
    {
        mainMenuUI.SetActive(true);
    }

    public void CloseMainMenu()
    {
        mainMenuUI.SetActive(false);
    }

    public Image GetLoadingBar()
    {
        return loadingBar;
    }

    public GameObject GetLoadingScreen()
    {
        return loadingScreen;
    }
}
