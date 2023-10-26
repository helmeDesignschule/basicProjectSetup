using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A general implementation of a loading screen.
//A more polished version would include a fade-in, in addition to the fade-out.
[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : MonoBehaviour
{
    //CanvasGroup is used to fade the loading screen in and out.
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeTime = .2f;

    //We track the number of instigators, meaning the number of classes that opened the loading screen.
    private static readonly List<object> LoadingScreenInstigators = new List<object>();
    
    public static void Show(object instigator)
    {
        LoadingScreenInstigators.Add(instigator);
        
        //If the instance is not yet set, that means that the ManagerScene was not loaded & initialized yet, meaning that 
        //we have to wait to show the screen.
        if (ScreenManager.Instance != null)
            ScreenManager.Instance.LoadingScreen.Show();
    }

    public static void Hide(object instigator)
    {
        LoadingScreenInstigators.Remove(instigator);
        
        //If the instance is not yet set, that means that the ManagerScene was not loaded & initialized yet, meaning that 
        //we have to wait to show the screen.
        if (ScreenManager.Instance != null && LoadingScreenInstigators.Count == 0)
            ScreenManager.Instance.LoadingScreen.Hide();
    }

    public void Initialize()
    {
        //If any instigators where registered before the initialization of the LoadingScreen
        //(which means, someone wanted to open the loadingScreen before the ManagerScene was loaded)
        //then show the loadingScreen immediately.
        //otherwise, hide the loading screen.
        if (LoadingScreenInstigators.Count > 0)
            Show();
        else
            gameObject.SetActive(false);
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        gameObject.SetActive(true);
        StopAllCoroutines(); //this stops the fadeout-coroutine.
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideCoroutine());
    }

    //in this coroutine, we fade out the loading screen over a certain time. 
    private IEnumerator HideCoroutine()
    {
        //this is a very typical implementation of how to move/fade/animate something over time.
        float time = 0;
        while (time < _fadeTime)
        {
            yield return null; //first, we wait until the next frame.
            time += Time.unscaledDeltaTime; //then, we increase the time since the fade start.
            
            //then, we divide the passed time by the complete duration.
            //this results in a percentage value between 0 and 1.
            //examples for _fadeTime = 2:
            //time = 0: 0/2 = 0     (we have not started)
            //time = 1: 1/2 = 0.5   (we are half-way there)
            //time = 2: 2/2 = 1     (we did it!)
            //in the end, we clamp the value, meaning it cannot be smaller than 0, or bigger than 1.
            float alpha = Mathf.Clamp01(time / _fadeTime); 
            
            //Before asigning the value, we invert the value by subtracting it from 1. 
            _canvasGroup.alpha = 1.0f - alpha;
        }

        //after we finished with the hide-loop, we disable the entire loadingScreen.
        _canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    //on validate is called within the editor whenever something about the LoadingScreen gameObject changes.
    //this makes it easy to automatically set or validate values.
    private void OnValidate()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }
}
