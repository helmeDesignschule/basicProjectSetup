using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is just a test script to show the loading screen for 2 seconds.
public class FakeLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        LoadingScreen.Show(this);
        yield return new WaitForSeconds(2);
        LoadingScreen.Hide(this);
    }
}
