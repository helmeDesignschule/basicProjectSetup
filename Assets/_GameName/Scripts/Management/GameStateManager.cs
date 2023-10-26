using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void StartGame()
    {
        Instance.StartCoroutine(Instance.LoadScenesCoroutine((int)SceneLoader.DefaultScenes.MainMenu, 3));
    }

    public static void GoToMainMenu()
    {
        Instance.StartCoroutine(Instance.LoadScenesCoroutine(3, (int)SceneLoader.DefaultScenes.MainMenu));
    }
    
    private IEnumerator LoadScenesCoroutine(int oldScene, int newScene)
    {
        LoadingScreen.Show(this);
        yield return SceneLoader.Instance.UnloadSceneViaIndex(oldScene);
        yield return SceneLoader.Instance.LoadSceneViaIndex(newScene);
        LoadingScreen.Hide(this);
    }
}
