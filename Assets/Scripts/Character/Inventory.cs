using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemCollector _itemCollector;
    private Dictionary<string, int> _inventory = new Dictionary<string, int>();
    public event Action<AidKid> AidKidCollected;

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
        if (item.TryGetComponent(out AidKid aidKid))
        {
            AidKidCollected?.Invoke(aidKid);
            return;
        }

        string itemName = item.Item_Name;

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
