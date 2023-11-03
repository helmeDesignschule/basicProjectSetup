using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryUIEntry : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;

    public event Action OnItemClicked;
    
    private Item _item;

    public void SetContent(Item item)
    {
        _item = item;
        _icon.sprite = _item.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnItemClicked != null)
            OnItemClicked();
    }
}
