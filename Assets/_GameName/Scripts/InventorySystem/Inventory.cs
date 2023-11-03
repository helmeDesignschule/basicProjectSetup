using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;
    
    private readonly List<Item> _items = new List<Item>();
    public IReadOnlyList<Item> Items => _items;

    private IEnumerator Start()
    {
        while (ScreenManager.Instance == null)
            yield return null;
        ScreenManager.Instance.RegisterPlayerInventory(this);
    }

    private void OnDestroy()
    {
        if (ScreenManager.Instance == null)
            return;
        ScreenManager.Instance.DeregisterPlayerInventory(this);
    }

    public void AddItem(Item item)
    {
        if (_items.Contains(item))
            return;
        
        _items.Add(item);
        
        if(OnItemAdded != null)
            OnItemAdded(item);
    }

    public void RemoveItem(Item item)
    {
        if (!_items.Contains(item))
            return;

        _items.Remove(item);

        if (OnItemRemoved != null)
            OnItemRemoved(item);
    }

    
}
