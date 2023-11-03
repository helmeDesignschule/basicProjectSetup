using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    //this is the startup manager. At the moment we load the Manager and the MainMenu scene.
    //But here would also any other setup logic go that would be needed at the startup of the game.
    private IEnumerator Start()
    {
        LoadingScreen.Show(this);
        yield return SceneLoader.LoadScene(SceneLoader.DefaultScenes.Manager);
        if (GameplayLoop.Instance == null)
            yield return SceneLoader.LoadScene(SceneLoader.DefaultScenes.MainMenu);
        LoadingScreen.Hide(this);
    }
}
