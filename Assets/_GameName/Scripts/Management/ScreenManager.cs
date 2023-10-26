using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a general manager for all UI screens.
public class ScreenManager : MonoBehaviour
{
    //this is a very quick and dirty implementation of a ScreenManager.
    public static ScreenManager Instance;

    [SerializeField] private LoadingScreen _loadingScreen;
    public LoadingScreen LoadingScreen => _loadingScreen;

    private void Awake()
    {
        Instance = this;
        _loadingScreen.Initialize();
    }
}
