using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : WeaponBase
{
    private float _enemyWeaponDamage = 15f;
    private void Awake()
    {
        _attackDamage = _enemyWeaponDamage;
        _playerMask = LayerMask.GetMask("Player");
    }
}
