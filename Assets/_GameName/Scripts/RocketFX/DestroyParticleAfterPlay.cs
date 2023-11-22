using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticleAfterPlay : MonoBehaviour
{
    private ParticleSystem _system;
    private void Awake()
    {
        _system = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        if (_system.IsAlive())
            return;
        
        Destroy(gameObject);
    }
}
