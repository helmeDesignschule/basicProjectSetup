using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController
{
    private readonly Inventory _inventory;
    private readonly InventoryUI _inventoryUI;
    
    public InventoryUIController(Inventory inventory, InventoryUI inventoryUI)
    {
        _inventory = inventory;
        _inventoryUI = inventoryUI;

        for (int index = 0; index < _inventory.Items.Count; index++)
        {
            inventoryUI.OnItemAdded(_inventory.Items[index]);
        }
        
        inventory.OnItemAdded += inventoryUI.OnItemAdded;
        inventory.OnItemRemoved += inventoryUI.OnItemRemoved;
    }

    public void Cleanup()
    {
        _inventory.OnItemAdded -= _inventoryUI.OnItemAdded;
        _inventory.OnItemRemoved -= _inventoryUI.OnItemRemoved;
        
        for (int index = 0; index < _inventory.Items.Count; index++)
        {
            _inventoryUI.OnItemRemoved(_inventory.Items[index]);
        }
    }
}
