using UnityEngine;
using System;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] protected string ItemName;

    public event Action<CollectibleItem> Collected;

    public string ItemNameLower => ItemName.ToLower();

    public void Collect()
    {
        Collected?.Invoke(this);
    }
}