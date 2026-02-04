using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly string _attack = "Attack";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(_attack);
    }
}
