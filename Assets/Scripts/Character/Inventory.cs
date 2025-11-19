using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemCollector _itemCollector;
    private Dictionary<string, int> _inventory = new Dictionary<string, int>();

    private void Awake()
    {
        _itemCollector = GetComponent<ItemCollector>();
    }

    private void OnEnable()
    {
        _itemCollector.ItemCollected += AddItem;
    }

    private void OnDisable()
    {
        _itemCollector.ItemCollected -= AddItem;
    }

    private void AddItem(CollectibleItem item)
    {
        string itemName = item.itemName;

        if (_inventory.ContainsKey(itemName))
        {
            _inventory[itemName]++;
        }
        else
        {
            _inventory[itemName] = 1;
        }
    }
}
