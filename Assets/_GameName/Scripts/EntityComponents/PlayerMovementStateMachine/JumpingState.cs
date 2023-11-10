using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the jumping state calculates the jump curve for the player.
/// </summary>
public class JumpingState : State
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;
    
    //Animation Curves allow to design your own curve in the editor, then give in a x value to get back a y value.
    [SerializeField] private AnimationCurve _jumpCurve = AnimationCurve.EaseInOut(0, 0, 0, 0);
    [SerializeField] private float _jumpDistance;

    private Vector3 _jumpStartPosition;
    private Vector3 _finalPosition;
    
    protected override void EnterInternal()
    {
        //at the beginning, we save the position the player starts their jump from.
        _jumpStartPosition = Owner.References.Transform.position;
        
        //then, we calculate where the player would land, if they would have started to jump from the last grounded position,
        //so that the player cannot jump too far, in case they used the coyote frames (jumping shortly after losing ground contact)
        _finalPosition = Owner.References.LastGroundedPosition + Owner.References.LookDirection * (_jumpDistance + _jumpDuration);
    }

    protected override void UpdateInternal()
    {
        //after the jump time runs out, we either enter the falling state, or the grounded state if we have ground under
        //our feet.
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
        //first, we calculate an alpha value. It is 0 at the start of the jump, and 1 if the jump was finished.
        float alpha = FixedTimeInState / _jumpDuration;

        //the actualY is the height of the jump curve, calculated from the jump start position
        float actualY = CalculateNewY(alpha, _jumpStartPosition.y);
        
        //the groundedY is the height of the jump curve, if the player would have started their jump at the last grounded position
        float groundedY = CalculateNewY(alpha, Owner.References.LastGroundedPosition.y);

        //then, we lerp the jump height from the current height to the actual height, making it so the player will always reach the
        //expected jump height, as if they started from the last grounded position
        Vector3 newPosition = Vector3.zero;
        newPosition.y = Mathf.Lerp(actualY, groundedY, Mathf.Clamp01(alpha * 2));
        
        //we lerp the horizontal positions, so that we move from the jump start position to the final position.
        //remember: the final position is calculated as if we would have started jumping from the last grounded position.
        newPosition.x = Mathf.Lerp(_jumpStartPosition.x, _finalPosition.x, alpha);
        newPosition.z = Mathf.Lerp(_jumpStartPosition.z, _finalPosition.z, alpha);
        
        //lastly, we calculate the movement that would happen this very frame.
        //with this distance, we can see if the player will collide with anything along their way.
        var movementDistance = newPosition - Owner.References.Position;
        
        if (Owner.Tools.CapsuleCast(movementDistance.normalized, movementDistance.magnitude, out var hit))
        {
            //if we hit anything in the way of the jump, we switch back to the grounded position
            //From there, we enter the falling state, if needed
            newPosition = Owner.References.Position + movementDistance.normalized * hit.distance;
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Grounded);
        }
        else
        {
            //if nothing is in the way, we move to the new position.
            Owner.References.Rigidbody.Move(newPosition, Quaternion.identity);
        }
    }

    //this function uses the jump curve and the current progress within the jump to calculate the current jump height.
    private float CalculateNewY(float alpha, float startY)
    {
        float newY = startY;
        //We evaluate the curve at the alpha value (a value between 0-1) and get the jump curve height back, which we also
        //set in the editor. We set the curve to a value between 0 and 1, so by multiplying it with the actual jump height,
        //We get the height we want.
        float wantedHeight = _jumpCurve.Evaluate(alpha) * _jumpHeight;
        newY += wantedHeight;

        return newY;
    }

    protected override void ExitInternal()
    {
    }
}
