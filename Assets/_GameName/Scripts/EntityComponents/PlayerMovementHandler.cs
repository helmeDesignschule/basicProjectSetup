using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a typical implementation of a StateMachine contained within a single script.
/// if you want to see an implementation that uses a multi-script setup, see ComplexCharacterMovementHandler.
/// </summary>
public class PlayerMovementHandler : MonoBehaviour
{
    public enum States
    {
        None = 0,
        Grounded = 1,
        Jumping = 2,
        Falling = 3,
    }

    private States _currentState = States.None;
    
    //we save every function in the corresponding array of actions. this way, we can access the functions
    //via the States-enum.
    private Action[] EnterState;
    private Action[] UpdateState;
    private Action[] ExitState;

    private float _timeInState;

    #region Initialisation
    private void Awake()
    {
        int stateCount = Enum.GetValues(typeof(States)).Length;
        EnterState = new Action[stateCount];
        UpdateState = new Action[stateCount];
        ExitState = new Action[stateCount];
        
        InitializeState(States.Grounded, EnterGrounded, UpdateGrounded, ExitGrounded);
        InitializeState(States.Jumping, EnterJumping, UpdateJumping, ExitJumping);
        InitializeState(States.Falling, EnterFalling, UpdateFalling, ExitFalling);
        
        SwitchState(States.Grounded);
    }

    private void InitializeState(States state, Action enter, Action update, Action exit)
    {
        EnterState[(int)state] = enter;
        UpdateState[(int)state] = update;
        ExitState[(int)state] = exit;
    }
    #endregion
    
    public void SwitchState(States newState)
    {
        if (newState == _currentState)
            return;

        Action exitCall = ExitState[(int)newState];
        if (exitCall != null)
            exitCall();

        _currentState = newState;
        _timeInState = 0;

        Action enterCall = EnterState[(int)_currentState];
        if (enterCall != null)
            enterCall();
    }

    private void Update()
    {
        _timeInState += Time.deltaTime;
        
        Action updateCall = UpdateState[(int)_currentState];
        if (updateCall != null)
            updateCall();
    }

    #region Grounded State
    private void EnterGrounded()
    {}
    
    private void UpdateGrounded()
    {}

    private void ExitGrounded()
    {}
    #endregion

    #region Jumping State
    private void EnterJumping()
    {}

    private void UpdateJumping()
    {}

    private void ExitJumping()
    {}
    #endregion

    #region Falling State
    private void EnterFalling()
    {}
    
    private void UpdateFalling()
    {}

    private void ExitFalling()
    {}
    #endregion
    
}
