using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State
{
    private Vector3 _runDirection;

    [SerializeField] private float _runSpeed;
    
    protected override void EnterInternal()
    {
    }

    protected override void UpdateInternal()
    {
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
        Owner.References.Rigidbody.velocity = Vector3.zero;
        
        if (_runDirection != Vector3.zero)
        {
            Owner.References.LookDirection = _runDirection;
            Owner.References.HorizontalVelocity = _runDirection * _runSpeed;
            Owner.References.Rigidbody.Move(Owner.References.Rigidbody.position + Owner.References.HorizontalVelocity * Time.fixedDeltaTime, Quaternion.identity);
        }
        else
        {
            Owner.References.HorizontalVelocity = Vector3.zero;
        }
        
        
        if (!Owner.Tools.CapsuleCast(Vector3.down, .1f, out var hit, .1f))
        {
            Owner.SwitchState(ComplexPlayerMovementHandler.States.Falling);
        }

        if (Physics.Raycast(Owner.References.Position + Vector3.up * .1f, Vector3.down, out hit, .2f))
        {
            Owner.References.LastGroundedPosition = hit.point;
        }
        
    }

    protected override void ExitInternal()
    {
    }
}
