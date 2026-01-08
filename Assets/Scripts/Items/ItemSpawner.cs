using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private CollectibleItem _itemPrefab;
    [SerializeField] private Transform _spawnPointsContainer;
    private Transform[] _spawnPoints;

    private void Start()
    {
        RefreshChildArray();
        InitiateItems();
    }

    private void InitiateItems()
    {
        foreach (var point in _spawnPoints)
        {
            CollectibleItem newItem = Instantiate(_itemPrefab, point.position, Quaternion.identity);
            newItem.Collected += DestroyItem;
        }
    }

    private void DestroyItem(CollectibleItem item)
    {
        Destroy(item.gameObject);
        item.Collected -= DestroyItem;
    }

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _spawnPointsContainer.childCount;
        _spawnPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            _spawnPoints[i] = _spawnPointsContainer.GetChild(i);
        }
    }
}
