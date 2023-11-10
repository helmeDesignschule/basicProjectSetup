using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is a demo class to show the Observer Pattern with a static callback, that returns
/// a reference to the instigator
/// </summary>
public class Enemy : MonoBehaviour
{
    //every script can subscribe to this event statically, via Enemy.OnEnemyDied.
    //The Enemy that is passed as parameter is the enemy instance that instigates the event. 
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
