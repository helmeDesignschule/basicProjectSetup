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

    private States _lastState;
    private States _currentState;

    [SerializeField] private PlayerReferences _references;
    public PlayerReferences References => _references;

    public PlayerTools Tools { get; private set; }
    
    [Header("States")]
    [SerializeField] private State GroundedState;
    [SerializeField] private State JumpingState;
    [SerializeField] private State FallingState;
    
    private void Awake()
    {
        var states = Enum.GetValues(typeof(States));
        foreach (var state in states)
        {
            var implementation = GetState((States)state);
            if(implementation != null)
                implementation.Initialize(this);
        }
        
        SwitchState(States.Grounded);
        Tools = new PlayerTools(_references);
    }

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
        if (switchToState == _currentState)
            return;
        
        var oldState = GetState(_currentState);
        if (oldState != null)
            oldState.ExitState();

        _lastState = _currentState;
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
    private void FixedUpdate()
    {
        var currentState = GetState(_currentState);
        if (currentState != null)
            currentState.FixedUpdateState();
    }
    
}
