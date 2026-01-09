using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Weapon _enemyWeapon;
    private string _playerTag = "Player";
    private Animator _animator;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _attackWait = new WaitForSeconds(1.5f);

    private void Awake()
    {
        _enemyWeapon = GetComponentInChildren<Weapon>();
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _enemyWeapon.AttackHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag))
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(AttackRoutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_playerTag))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (enabled)
        {
            _animator.SetTrigger("Attack");
            Attack();
            yield return _attackWait;
        }
    }
}
