using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDied;

    private void OnEnable()
    {
        GetComponent<Health>().OnHealthChanged += TestForDeath;
    }

    private void TestForDeath(float health)
    {
        if (health <= 0)
        {
            if (OnEnemyDied != null)
                OnEnemyDied(this);
            
            Destroy(gameObject);
        }
    }
}
