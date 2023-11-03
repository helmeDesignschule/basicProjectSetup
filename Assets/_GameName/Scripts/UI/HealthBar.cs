using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthVisuals;

    private void OnEnable()
    {
        _health.RegisterForOnMaximumHealthChanged(OnMaximumHealthChanged, true);
        _health.RegisterForOnHealthChanged(OnHealthChanged, true);
    }

    private void OnDisable()
    {
        _health.OnMaximumHealthChanged -= OnMaximumHealthChanged;
        _health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        _healthVisuals.value = health;
    }

    private void OnMaximumHealthChanged(float health)
    {
        _healthVisuals.maxValue = health;
    }
}
