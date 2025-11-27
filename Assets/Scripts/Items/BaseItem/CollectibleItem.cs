using UnityEngine;
using System;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string _itemName;

    public event Action<CollectibleItem> Collected;

    public string Item_Name => _itemName.ToLower();

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}
