using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;

    [SerializeField] private LoadingScreen _loadingScreen;
    public LoadingScreen LoadingScreen => _loadingScreen;

    private void Awake()
    {
        Instance = this;
        _loadingScreen.Initialize();
    }
}
