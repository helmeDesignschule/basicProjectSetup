using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private ParticleSystem _trailSystem;
    [SerializeField] private AnimationCurve _directProgressCurve;
    [SerializeField] private AnimationCurve _heightOffsetCurve;
    [SerializeField] private float _rotationOffsetRange;

    private Transform _transform;
    private Vector3 _lastPosition;
    
    public void Launch(Vector3 targetPosition, float flyTime)
    {
        _transform = transform;
        _lastPosition = _transform.position;

        var rocketParent = new GameObject("Rocket Parent").transform;
        rocketParent.position = targetPosition;
        rocketParent.forward = (targetPosition - _lastPosition).normalized;

        _transform.SetParent(rocketParent);
        
        var randomRotationOffset = Random.Range(-_rotationOffsetRange, _rotationOffsetRange);
        // rocketParent.Rotate(rocketParent.forward, randomRotationOffset);
        var rotation = rocketParent.localEulerAngles;
        rotation.z += randomRotationOffset;
        rocketParent.localEulerAngles = rotation;
        
        
        StartCoroutine(FlyTowardsTarget(targetPosition, flyTime));
    }

    private void LateUpdate()
    {
        var moveDirection = _transform.position - _lastPosition;
        moveDirection.Normalize();
        if (moveDirection.sqrMagnitude > 0)
            _transform.forward = moveDirection;
        _lastPosition = _transform.position;
    }

    private IEnumerator FlyTowardsTarget(Vector3 targetPosition, float flyTime)
    {
        Vector3 startPosition = _transform.localPosition;
        float alpha = 0;

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / flyTime;
            Vector3 newPosition = Vector3.Lerp(startPosition, Vector3.zero, _directProgressCurve.Evaluate(alpha));
            newPosition.y += _heightOffsetCurve.Evaluate(alpha);
            _transform.localPosition = newPosition;
            yield return null;
        }

        Instantiate(_explosionPrefab, targetPosition, Quaternion.identity);
        while (_trailSystem.particleCount > 0)
            yield return null;
        Destroy(_transform.parent.gameObject);
    }
}
