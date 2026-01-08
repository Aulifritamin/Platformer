using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotator))]
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private CharacterRotator _rotator;
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField] private CheckRange _checkRange;

    private void Awake()
    {
        _characterMover = GetComponent<CharacterMover>();
        _rotator = GetComponent<CharacterRotator>();
    }

    public void FollowTarget(Transform target)
    {
        if(target == null)
        {
            return;
        }
        
        float direction = Mathf.Sign(target.position.x - transform.position.x);

        _characterMover.Move(direction);
        _rotator.Rotate(direction);
        _checkRange.SetTarget(target);  
    }
}
