using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeTime = .2f;

    private static List<object> LoadingScreenInstigators = new List<object>();
    

    public static void Show(object instingator)
    {
        LoadingScreenInstigators.Add(instingator);
        if (ScreenManager.Instance != null)
            ScreenManager.Instance.LoadingScreen.Show();
    }

    public static void Hide(object instigator)
    {
        LoadingScreenInstigators.Remove(instigator);
        if (ScreenManager.Instance != null && LoadingScreenInstigators.Count == 0)
            ScreenManager.Instance.LoadingScreen.Hide();
    }

    public void Initialize()
    {
        if (LoadingScreenInstigators.Count > 0)
        {
            Show();
        }
        else
            gameObject.SetActive(false);
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        float time = 0;
        while (time < _fadeTime)
        {
            yield return null;
            time += Time.unscaledDeltaTime;
            _canvasGroup.alpha = 1.0f - Mathf.Clamp01(time / _fadeTime);
        }

        _canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }
}
