using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state that handles the falling of the character.
/// </summary>
public class FallingState : State
{
    [SerializeField] private float _fallAcceleration;

    [SerializeField] private float _maximumFallSpeed;
    [SerializeField] private float _coyoteTime = 1f;

    private float _startFallSpeed;
    private Vector3 _fallStartPosition;
    
    protected override void EnterInternal()
    {
        //we save the start of the falling position, so that we can accurately calculate the falling positions.
        _fallStartPosition = Owner.References.Position;
    }

    protected override void UpdateInternal()
    {
        if (TimeInState <= _coyoteTime && Input.GetKeyDown(KeyCode.Space))
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Jumping);
    }

    protected override void FixedUpdateInternal()
    {
        var newPosition = _fallStartPosition;

        //first, we calculate the current y position, by taking the falling time and applying the gravity accordingly
        newPosition.y -= _fallAcceleration * FixedTimeInState * FixedTimeInState;

        //then we calculate the vertical movement, dependant on the current horizontal velocity.
        var newHorizontalPosition = Owner.References.Position;
        newHorizontalPosition += Owner.References.HorizontalVelocity * Time.fixedDeltaTime;
        newPosition.x = newHorizontalPosition.x;
        newPosition.z = newHorizontalPosition.z;

        //to test if nothing is in our way, we calculate how far the character would move from the current position
        //to the newly calculated position.
        var movementDistance = newPosition - Owner.References.Position;

        //we make a capsule cast to test if the path is clear
        if (Owner.Tools.CapsuleCast(movementDistance.normalized, movementDistance.magnitude, out var hit))
        {
            //if the path is not clear, we move the character only by the distance of the call.
            newPosition = Owner.References.Position + movementDistance.normalized * hit.distance;
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Grounded);
        }
        else
        {
            //otherwise, we move the character to the newly calculated position.
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
        }
    }

    protected override void ExitInternal()
    {
    }
}
