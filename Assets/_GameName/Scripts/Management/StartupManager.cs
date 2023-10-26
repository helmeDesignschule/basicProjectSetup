using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        LoadingScreen.Show(this);
        yield return SceneLoader.LoadScene(SceneLoader.DefaultScenes.Manager);
        yield return SceneLoader.LoadScene(SceneLoader.DefaultScenes.MainMenu);
        LoadingScreen.Hide(this);
    }
}
