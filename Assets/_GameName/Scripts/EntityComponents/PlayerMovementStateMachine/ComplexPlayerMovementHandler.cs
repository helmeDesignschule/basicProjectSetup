using System;
using UnityEngine;

public class ComplexPlayerMovementHandler : MonoBehaviour
{
    
    //we use a enum reference for the states.
    //we could also just use reference to the states themselves.
    public enum States
    {
        None,
        Grounded,
        Jumping,
        Falling,
    }

    //we don't use the last state yet, but it is often handy to know from what state another state was entered.
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
        //we get all enums from the states, and then initialize them all.
        var states = Enum.GetValues(typeof(States));
        foreach (var state in states)
        {
            var implementation = GetState((States)state);
            if(implementation != null)
                implementation.Initialize(this);
        }
        
        SwitchState(States.Grounded); //first state will be the grounded state.
        Tools = new PlayerTools(_references);
    }

    //we use this class to get the actual implementation via the enum.
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
        //if we want to switch to a state we are already in, we ignore the call.
        if (switchToState == _currentState)
            return;
        
        //if we are in a state, we call the exit function of that state.
        var oldState = GetState(_currentState);
        if (oldState != null)
            oldState.ExitState();

        //then, we save what the new state is.
        _lastState = _currentState;
        _currentState = switchToState;

        //in the end, we call the enter function of the new state.
        var newState = GetState(_currentState);
        if (newState != null)
            newState.EnterState();
    }

    private void Update()
    {
        //if we are in a valid state, we call its update function
        var currentState = GetState(_currentState);
        if (currentState != null)
            currentState.UpdateState();
    }
    private void FixedUpdate()
    {
        //if we are in a valid state, we call its fixed update function
        var currentState = GetState(_currentState);
        if (currentState != null)
            currentState.FixedUpdateState();
    }
    
}
