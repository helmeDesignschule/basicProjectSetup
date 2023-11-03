using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexPlayerMovementHandler : MonoBehaviour
{
    public enum States
    {
        None,
        Grounded,
        Jumping,
        Falling,
    }

    private States _currentState;
    
    [SerializeField] private State GroundedState;
    [SerializeField] private State JumpingState;
    [SerializeField] private State FallingState;

    public State GetState(States state)
    {
        switch (state)
        {
            case States.Grounded:
                return GroundedState;
            case States.Jumping:
                return JumpingState;
            case States.Falling:
                return FallingState;
            default:
                return null;
        }
    }
    
    public void SwitchState(States switchToState)
    {
        var oldState = GetState(_currentState);
        if (oldState != null)
            oldState.ExitState();

        _currentState = switchToState;

        var newState = GetState(_currentState);
        if (newState != null)
            newState.EnterState();
    }

    private void Update()
    {
        var currentState = GetState(_currentState);
        if (currentState != null)
            currentState.UpdateState();
    }
}
