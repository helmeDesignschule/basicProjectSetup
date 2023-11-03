using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action<float> OnHealthChanged;

    public event System.Action<float> OnMaximumHealthChanged;

    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _currentHealth = 10;

    public float CurrentHealth
    {
        get => _currentHealth;
        set => SetCurrentHealth(value);
    }

    public float MaximumHealth
    {
        get => _maximumHealth;
        set => SetMaximumHealth(value);
    }

    private void Awake()
    {
        SetCurrentHealth(_maximumHealth);
    }

    public void AddHealth(float additionalHealth)
    {
        SetCurrentHealth(_currentHealth + additionalHealth);
    }
    
    public void SetCurrentHealth(float newHealth)
    {
        if (newHealth > _maximumHealth)
            newHealth = _maximumHealth;
        
        var oldHealth = _currentHealth;
        _currentHealth = newHealth;
        
        if (OnHealthChanged != null)
            OnHealthChanged(_currentHealth);
    }

    public void SetMaximumHealth(float newMaximum)
    {
        _maximumHealth = newMaximum;
        if (OnMaximumHealthChanged != null)
            OnMaximumHealthChanged(newMaximum);
        
        if (_currentHealth > newMaximum)
            SetCurrentHealth(newMaximum);
    }

    public void RegisterForOnHealthChanged(Action<float> callback, bool getInstantCallback = false)
    {
        OnHealthChanged += callback;
        if (getInstantCallback)
            callback(_currentHealth);
    }

    public void RegisterForOnMaximumHealthChanged(Action<float> callback, bool getInstantCallback = false)
    {
        OnMaximumHealthChanged += callback;
        if (getInstantCallback)
            callback(_maximumHealth);
    }
}
