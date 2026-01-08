using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Coin : CollectibleItem
{
    private string _coinName = "Coin";
    private void Awake()
    {
        _itemName = _coinName;
    }
}