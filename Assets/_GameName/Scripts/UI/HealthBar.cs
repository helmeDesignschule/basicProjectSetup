using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// a script used to demonstrate the observer pattern. This HealthBar is the observer, in this case,
/// observing the health script.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthVisuals;

    private void OnEnable()
    {
        //on enable, we register the callbacks with the InstantCallback function enabled.
        //this way, our health bar gets set up correctly immediately.
        _health.RegisterForOnMaximumHealthChanged(OnMaximumHealthChanged, true);
        _health.RegisterForOnHealthChanged(OnHealthChanged, true);
    }

    private void OnDisable()
    {
        //on disable, we deregister the callbacks. If we don't deregister, the calls will always trigger, even when
        //the UI is disabled, causing unnecessary performance strain.
        //worse, if the UI gets destroyed without being deregistered, the callbacks will cause null reference exceptions.
        _health.OnMaximumHealthChanged -= OnMaximumHealthChanged;
        _health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        //this function is called by the callback, updating the UI.
        _healthVisuals.value = health;
    }

    private void OnMaximumHealthChanged(float health)
    {
        //this function is called by the callback, updating the UI.
        _healthVisuals.maxValue = health;
    }
}
