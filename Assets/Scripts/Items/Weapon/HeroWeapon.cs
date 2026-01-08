using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapon : WeaponBase
{
    private float _heroWeaponDamage = 25f;
    private void Awake()
    {
        _playerMask = LayerMask.GetMask("Enemy");
        _attackDamage = _heroWeaponDamage;
    }
}