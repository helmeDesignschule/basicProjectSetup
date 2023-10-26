using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class SceneLoader : MonoBehaviour
{
    public enum DefaultScenes
    {
        Startup = 0,
        Manager = 1,
        MainMenu = 2,
    }

    private static SceneLoader _instance;

    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SceneLoader").AddComponent<SceneLoader>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public Coroutine LoadSceneViaIndex(DefaultScenes scene, Action onLoadingFinished = null)
    {
        return LoadSceneViaIndex((int)scene, onLoadingFinished);
    }

    public Coroutine LoadSceneViaIndex(int index, Action onLoadingFinished = null)
    {
        return StartCoroutine(LoadSceneViaIndexCoroutine(index, onLoadingFinished));
    }

    
    private IEnumerator LoadSceneViaIndexCoroutine(int index, Action onLoadingFinished)
    {
        var scene = SceneManager.GetSceneByBuildIndex(index);
        if (scene.isLoaded)
        {
            onLoadingFinished?.Invoke();
            yield break;
        }
        
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        onLoadingFinished?.Invoke();
    }

    public static Coroutine LoadScene(DefaultScenes scene, Action onLoadingFinished = null)
    {
        return Instance.StartCoroutine(Instance.LoadSceneViaIndexCoroutine((int)scene, onLoadingFinished));
    }
    
    public Coroutine UnloadSceneViaIndex(int index, Action onLoadingFinished = null)
    {
        return StartCoroutine(UnloadSceneViaIndexCoroutine(index, onLoadingFinished));
    }
    
    private IEnumerator UnloadSceneViaIndexCoroutine(int index, Action onLoadingFinished)
    {
        var scene = SceneManager.GetSceneByBuildIndex(index);
        if (!scene.isLoaded)
        {
            onLoadingFinished?.Invoke();
            yield break;
        }
        
        yield return SceneManager.UnloadSceneAsync(index);
        onLoadingFinished?.Invoke();
    }
    
    #if UNITY_EDITOR
    [MenuItem("OurGame/Load Startup Scene", false)]
    private static void LoadStartupScene()
    {
        var scene = EditorBuildSettings.scenes[(int)DefaultScenes.Startup];
        EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Additive);
    }
    #endif
    
}
