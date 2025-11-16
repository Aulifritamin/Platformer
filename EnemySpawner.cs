using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPointsContainer;
    private Transform[] _spawnPoints;
    
    private void Start()
    {
        RefreshChildArray();
        InitiateEnemies();
    }

    private void InitiateEnemies()
    {
        foreach (var point in _spawnPoints)
        {
            Instantiate(_enemyPrefab, point.position, Quaternion.identity);
        }
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
