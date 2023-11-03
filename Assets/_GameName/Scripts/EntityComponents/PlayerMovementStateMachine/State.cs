using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected float TimeInState;
    
    public void EnterState()
    {
        TimeInState = 0;
        EnterInternal();
    }

    protected abstract void EnterInternal();

    public void UpdateState()
    {
        TimeInState += Time.deltaTime;
        UpdateInternal();
    }

    protected abstract void UpdateInternal();

    public void ExitState()
    {
        ExitInternal();
    }

    protected abstract void ExitInternal();
}
