using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKid : CollectibleItem
{
    private string _aidKitName = "AidKid";
    
    public float HealAmount { get; } = 30f;

    private void Awake()
    {
        ItemName = _aidKitName;
    }
}
