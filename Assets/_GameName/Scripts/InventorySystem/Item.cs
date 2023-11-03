using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string _displayName;
    public string DisplayName
    {
        get
        {
            if (string.IsNullOrEmpty(_displayName))
                return "-name not assigned-";
            return _displayName;
        }
    }
    
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    private void OnEnable()
    {
        var inventory = GetComponentInParent<Inventory>();
        if (inventory == null)
            return;
        
        inventory.AddItem(this);
    }

    private void OnDisable()
    {
        var inventory = GetComponentInParent<Inventory>();
        if (inventory == null)
            return;
        
        inventory.RemoveItem(this);
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(gameObject.scene.path))
            return;
        
        name = $"[Item] {_displayName}";
    }
    
    public void Eat()
    {
        Destroy(gameObject);
    }
}
