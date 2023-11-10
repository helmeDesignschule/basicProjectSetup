using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the health implementation, to show the observer pattern in action.
/// </summary>
public class Health : MonoBehaviour
{
    //this are the callbacks where we can register any listener that cares about health changes.
    //Some examples for possible listeners:
    //Character Controllers that handle the death, etc
    //The health bar or other UI components
    //Items that activate at certain health thresholds
    //Achievement system that cares about certain healthbased achievements
    public event System.Action<float> OnHealthChanged;

    public event System.Action<float> OnMaximumHealthChanged;

    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _currentHealth = 10;

    public float CurrentHealth
    {
        get => _currentHealth;
        //with a setter, we can make sure that all callbacks are called correcty
        set => SetCurrentHealth(value);
    }

    public float MaximumHealth
    {
        get => _maximumHealth;
        //with a setter, we can make sure that all callbacks are called correcty
        set => SetMaximumHealth(value);
    }

    private void Awake()
    {
        //We want to always start with full health
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
        
        _currentHealth = newHealth;
        
        //now that the health is changed, lets trigger the callback.
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

    //we can add Register-Methods. This way, any observer can have an instant callback, getting immediately
    //notified about the current value.
    public void RegisterForOnHealthChanged(System.Action<float> callback, bool getInstantCallback = false)
    {
        OnHealthChanged += callback;
        if (getInstantCallback)
            callback(_currentHealth);
    }

    public void RegisterForOnMaximumHealthChanged(System.Action<float> callback, bool getInstantCallback = false)
    {
        OnMaximumHealthChanged += callback;
        if (getInstantCallback)
            callback(_maximumHealth);
    }
}
