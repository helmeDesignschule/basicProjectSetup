using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        LoadingScreen.Show(this);
        yield return new WaitForSeconds(2);
        LoadingScreen.Hide(this);
    }
}
