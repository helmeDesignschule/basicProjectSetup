using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base state from which all other states derive.
/// Note: it is abstract, meaning that it can not instantiated itself. Only child classes can be instantiated.
/// </summary>
public abstract class State : MonoBehaviour
{
    protected ComplexPlayerMovementHandler Owner;
    
    /// <summary>
    /// The time since the state was entered.
    /// </summary>
    protected float TimeInState;
    
    /// <summary>
    /// The fixed time since the state was entered, for any physics tests.
    /// </summary>
    protected float FixedTimeInState;

    public void Initialize(ComplexPlayerMovementHandler owner)
    {
        Owner = owner;
    }
    
    //The public State-Calls are done form the State Machine handler (in this case the ComplexPlayerMovementHandler)
    //It handles all the basic logic for the EnterState-logic, then calls the Internal version of itself.
    //The internal version are then overridden by the child-class.
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
