using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : State
{
    [SerializeField] private float _fallAcceleration;

    [SerializeField] private float _maximumFallSpeed;
    [SerializeField] private float _coyoteTime = 1f;

    private float _startFallSpeed;
    private Vector3 _fallStartPosition;
    
    protected override void EnterInternal()
    {
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
        newPosition.y -= _fallAcceleration * FixedTimeInState * FixedTimeInState;

        var newHorizontalPosition = Owner.References.Position;
        newHorizontalPosition += Owner.References.HorizontalVelocity * Time.fixedDeltaTime;
        newPosition.x = newHorizontalPosition.x;
        newPosition.z = newHorizontalPosition.z;

        var movementDistance = newPosition - Owner.References.Position;

        if (Owner.Tools.CapsuleCast(movementDistance.normalized, movementDistance.magnitude, out var hit))
        {
            newPosition = Owner.References.Position + movementDistance.normalized * hit.distance;
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Grounded);
        }
        else
        {
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
        }
    }

    protected override void ExitInternal()
    {
    }
}
