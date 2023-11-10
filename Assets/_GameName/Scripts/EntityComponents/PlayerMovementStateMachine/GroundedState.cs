using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// in the grounded state, the player can move about, and jump.
/// Once they jumped, or lost ground contact, they will exit this state.
/// </summary>
public class GroundedState : State
{
    private Vector3 _runDirection;

    [SerializeField] private float _runSpeed;
    
    protected override void EnterInternal()
    {
    }

    protected override void UpdateInternal()
    {
        //we save the movement input in the update method, and use it in the fixed update method.
        //we do this, because input becomes imprecise if handled in the FixedUpdate State. 
        if (Input.GetKey(KeyCode.A))
            _runDirection = Vector3.left;
        else if (Input.GetKey(KeyCode.D))
            _runDirection = Vector3.right;
        else
            _runDirection = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.Space))
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Jumping);
    }

    protected override void FixedUpdateInternal()
    {
        //we stop the kinetic energy of the rigid body, so that we can move it ourself
        Owner.References.Rigidbody.velocity = Vector3.zero;
        
        if (_runDirection != Vector3.zero)
        {
            //if we have a run input, we move the character via the Move function, so that we have precise control over the movement.
            Owner.References.LookDirection = _runDirection;
            
            //we save the horizontal velocity, so that other states can use it (i.E. the falling state).
            Owner.References.HorizontalVelocity = _runDirection * _runSpeed;
            Owner.References.Rigidbody.Move(Owner.References.Rigidbody.position + Owner.References.HorizontalVelocity * Time.fixedDeltaTime, Quaternion.identity);
        }
        else
        {
            //if we dont move, the horizontal velocity is zero.
            Owner.References.HorizontalVelocity = Vector3.zero;
        }
        
        //in the end, we capsule cast to see if we are still grounded. if not, we start falling.
        if (!Owner.Tools.CapsuleCast(Vector3.down, .1f, out var hit, .1f))
        {
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Falling);
        }

        //we raycast if our center is grounded.
        //Many games will not allow a "half-grounded" state, because it will mess up foot placement for the character.
        //so, often, if more than half the character would hang over the edge, the game would either push them off completely,
        //or pull them back onto the ledge.
        if (Physics.Raycast(Owner.References.Position + Vector3.up * .1f, Vector3.down, out hit, .2f))
        {
            Owner.References.LastGroundedPosition = hit.point;
        }
        
    }

    protected override void ExitInternal()
    {
    }
}
