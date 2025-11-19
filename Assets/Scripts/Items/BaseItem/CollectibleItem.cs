using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string _itemName;

    public string itemName => _itemName.ToLower();

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
