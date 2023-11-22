using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class OffsetNoise : MonoBehaviour
{
    [SerializeField] private Transform _rotator;
    [SerializeField] private Transform _oscillator;
    [SerializeField] private float  _oscillatorAmplitude = 2;
    [SerializeField] private float _rotationSpeedMultiplier = .01f;
    [SerializeField] private float _oscillationSpeedMultiplier = .11f;

    private float _timeSinceStart = 0;
    
    void Update()
    {
        if (_rotator == null || _oscillator == null)
            return;
        
        _timeSinceStart += Time.deltaTime;
        
        //Perlin Noise is a noise function that returns a value between 0 and 1, corresponding to the value you put in.
        //This way, we can get an alpha value with which we can calculate rotation and amplitude.
        var rotationNoise = Mathf.PerlinNoise1D(_timeSinceStart * _rotationSpeedMultiplier);
        var oscillationNoise = Mathf.PerlinNoise1D(_timeSinceStart * _oscillationSpeedMultiplier);

        var rotatorEuler = _rotator.localEulerAngles;
        rotatorEuler.z = 360 * rotationNoise * 4;
        _rotator.localEulerAngles = rotatorEuler;

        _oscillator.localPosition = new Vector3(0, oscillationNoise * (_oscillatorAmplitude - .5f), 0);
    }
}
