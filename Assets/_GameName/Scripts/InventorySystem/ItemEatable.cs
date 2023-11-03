using System.Collections;
using System.Collections.Generic;
using _GameName.Scripts.InventorySystem;
using UnityEngine;

public class ItemEatable : Item, IEatable
{
    public void Eat()
    {
        Destroy(gameObject);
    }
}
