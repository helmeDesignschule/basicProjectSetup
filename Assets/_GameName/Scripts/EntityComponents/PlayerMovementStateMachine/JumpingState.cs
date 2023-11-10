using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private AnimationCurve _jumpCurve = AnimationCurve.EaseInOut(0, 0, 0, 0);
    [SerializeField] private float _jumpDistance;

    private Vector3 _jumpStartPosition;
    private Vector3 _finalPosition;
    
    protected override void EnterInternal()
    {
        _jumpStartPosition = Owner.References.Transform.position;
        _finalPosition = Owner.References.LastGroundedPosition + Owner.References.LookDirection * (_jumpDistance + _jumpDuration);
    }

    protected override void UpdateInternal()
    {
        if (TimeInState > _jumpDuration)
        {
            if (!Owner.Tools.CapsuleCast(Vector3.down, .1f, out var hit, .1f))
            {
                Owner.SwitchState(ComplexPlayerMovementHandler.States.Falling);
            }
            else
            {
                Owner.SwitchState(ComplexPlayerMovementHandler.States.Grounded);
            }
        }
    }

    protected override void FixedUpdateInternal()
    {
        float alpha = FixedTimeInState / _jumpDuration;

        
        float actualY = CalculateNewY(alpha, _jumpStartPosition.y);
        float groundedY = CalculateNewY(alpha, Owner.References.LastGroundedPosition.y);

        Vector3 newPosition = Vector3.zero;
        newPosition.y = Mathf.Lerp(actualY, groundedY, Mathf.Clamp01(alpha * 2));
        
        newPosition.x = Mathf.Lerp(_jumpStartPosition.x, _finalPosition.x, alpha);
        newPosition.z = Mathf.Lerp(_jumpStartPosition.z, _finalPosition.z, alpha);
        
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

    private float CalculateNewY(float alpha, float startY)
    {
        float newY = startY;
        float wantedHeight = _jumpCurve.Evaluate(alpha) * _jumpHeight;
        newY += wantedHeight;

        return newY;
    }

    protected override void ExitInternal()
    {
    }
}
