using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected ComplexPlayerMovementHandler Owner;
    protected float TimeInState;
    protected float FixedTimeInState;

    public void Initialize(ComplexPlayerMovementHandler owner)
    {
        Owner = owner;
    }
    
    public void EnterState()
    {
        TimeInState = 0;
        FixedTimeInState = 0;
        EnterInternal();
    }

    protected abstract void EnterInternal();

    public void UpdateState()
    {
        TimeInState += Time.deltaTime;
        UpdateInternal();
    }

    protected abstract void UpdateInternal();

    public void FixedUpdateState()
    {
        FixedTimeInState += Time.fixedDeltaTime;
        FixedUpdateInternal();
    }

    protected abstract void FixedUpdateInternal();

    public void ExitState()
    {
        ExitInternal();
    }

    protected abstract void ExitInternal();
}
