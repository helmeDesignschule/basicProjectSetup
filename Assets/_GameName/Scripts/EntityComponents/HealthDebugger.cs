using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthDebugger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Health>().CurrentHealth += 5;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Health>().CurrentHealth -= 5;
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Health>().MaximumHealth += 5;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Health>().MaximumHealth -= 5;
        }
    }
}
