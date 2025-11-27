using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public event Action<CollectibleItem> ItemCollected;

    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.TryGetComponent(out CollectibleItem pickupItem))
        {
            ItemCollected?.Invoke(pickupItem);
            pickupItem.Collect();
        }
    }
}
