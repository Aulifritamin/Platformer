using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Weapon _enemyWeapon;
    private EnemyAnimator _animator;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _attackWait = new WaitForSeconds(1.5f);

    private void Awake()
    {
        _enemyWeapon = GetComponentInChildren<Weapon>();
        _animator = GetComponent<EnemyAnimator>();
    }

    public void Attack()
    {
        _enemyWeapon.AttackHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character _))
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attacking());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character _))
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }

    private IEnumerator Attacking()
    {
        while (enabled)
        {
            _animator.SetAttackTrigger();
            Attack();
            yield return _attackWait;
        }
    }
}
