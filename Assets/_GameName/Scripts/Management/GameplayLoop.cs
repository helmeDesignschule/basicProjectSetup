using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoop : MonoBehaviour
{
    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }

    private void Update()
    {
        if (Input.anyKey)
            ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        GameStateManager.GoToMainMenu();
    }
}
