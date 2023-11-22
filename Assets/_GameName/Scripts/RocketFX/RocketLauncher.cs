using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private float _durationUntilImpact = 1;
    [SerializeField] private int _rocketCount = 3;
    [SerializeField] private float _delayBetweenRockets = .2f;
    [SerializeField] private Rocket _rocketPrefab;
    
    
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit))
            return;

        StartCoroutine(SendOutRockets(hit.point));
    }

    private IEnumerator SendOutRockets(Vector3 target)
    {
        float delay = _delayBetweenRockets;
        if (_delayBetweenRockets * _rocketCount >= _durationUntilImpact)
            delay = _durationUntilImpact / (_rocketCount + 1);
        
        for (int index = 0; index < _rocketCount; ++index)
        {
            Shoot(transform.position, target, _durationUntilImpact - index * delay);
            yield return new WaitForSeconds(delay);
        }
    }
    
    public void Shoot(Vector3 startPosition, Vector3 endPosition, float flightDuration)
    {
        var rocket = Instantiate(_rocketPrefab, startPosition, Quaternion.identity);
        rocket.Launch(endPosition, flightDuration);
    }    
}
