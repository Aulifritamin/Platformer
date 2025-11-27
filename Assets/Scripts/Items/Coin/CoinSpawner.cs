using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CollectibleItem _coinPrefab;
    [SerializeField] private Transform _spawnPointsContainer;
    private Transform[] _spawnPoints;
    
    private void Start()
    {
        RefreshChildArray();
        InitiateCoins();
    }

    private void InitiateCoins()
    {
        foreach (var point in _spawnPoints)
        {
            CollectibleItem newCoin = Instantiate(_coinPrefab, point.position, Quaternion.identity);
            newCoin.Collected += DestroyItem;
        }
    }

    private void DestroyItem(CollectibleItem coin)
    {
        Destroy(coin.gameObject);
        coin.Collected -= DestroyItem;
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
