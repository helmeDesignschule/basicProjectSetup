using System.Collections;
using System.Collections.Generic;
using _GameName.Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private RectTransform _entryParent;
    [SerializeField] private InventoryUIEntry _entryPrefab;

    private readonly Dictionary<Item, InventoryUIEntry> _entriesByItems = new Dictionary<Item, InventoryUIEntry>();

    public void OnItemAdded(Item item)
    {
        Debug.Log($"Added item: {item.DisplayName}", item);

        var newEntry = Instantiate(_entryPrefab, _entryParent);
        newEntry.SetContent(item);

        newEntry.OnItemClicked += item.Eat;
        
        _entriesByItems.Add(item, newEntry);
    }

    public void OnItemRemoved(Item item)
    {
        Debug.Log($"Removed item: {item.DisplayName}", item);
        
        if (_entriesByItems.ContainsKey(item))
        {
            InventoryUIEntry entry = _entriesByItems[item];
            Destroy(entry.gameObject);
        }
    }
}
