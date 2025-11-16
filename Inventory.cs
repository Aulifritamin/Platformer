using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Coin Coin;
    private int _coinCount;

    private void OnEnable()
    {
        Coin.Collected += OnCoinCollected;
    }

    private void OnDisable()
    {
        Coin.Collected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        _coinCount++;
        Debug.Log("Coins collected: " + _coinCount);
    }

}
