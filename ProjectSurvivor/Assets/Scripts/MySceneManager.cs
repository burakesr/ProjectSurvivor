using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager>
{
    [SerializeField]
    private GameObject loadingScreen;

    public event Action OnMenuSceneLoaded;
    public event Action OnGameplaySceneLoaded;

    private float totalSceneProgress;
    
    private void Start()
    {
        if (GameManager.Instance.isTestBuild) return;

        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuRoutine());
    }

    private IEnumerator LoadMainMenuRoutine()
    {
        AsyncOperation loadMenu = SceneManager.LoadSceneAsync((int)SceneIndexes.MENU, LoadSceneMode.Additive);

        while (!loadMenu.isDone)
        {
            yield return null;
        }

        UIManager.Instance.OpenMainMenu();
    }


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        UIManager.Instance.CloseMainMenu();
        GameManager.Instance.GetCameras().SetActive(true);
        UIManager.Instance.GetLoadingScreen().SetActive(true);

        
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.GAMEPLAY, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (var operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                UIManager.Instance.GetLoadingBar().fillAmount = totalSceneProgress;

                yield return null;
            }
        }

        OnGameplaySceneLoaded?.Invoke();
        loadingScreen.SetActive(false);
        GameManager.Instance.InitialisePlayer();
        UIManager.Instance.OpenGameplayUI();

    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}