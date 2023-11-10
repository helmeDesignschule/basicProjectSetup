using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    public Rigidbody Rigidbody => _rigidBody;

    [SerializeField] private Transform _transform;
    public Transform Transform => _transform;
    public Vector3 Position => _transform.position;

    public Vector3 LookDirection;
    public Vector3 HorizontalVelocity;
    public Vector3 LastGroundedPosition;
}
