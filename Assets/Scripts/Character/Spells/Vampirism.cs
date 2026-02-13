using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputListener))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _healAmount = 4f;
    [SerializeField] private float _damageAmount = 8f;
    [SerializeField] private float _duration = 6f; 
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private VampirismEffect _vampirimsArea;

    [SerializeField] private Health _health;

    private bool _isActive = false;
    private WaitForSeconds _attackInterval = new WaitForSeconds(1f);
    private WaitForSeconds _oneSecondWait = new WaitForSeconds(1f);
    private Coroutine _attackingCoroutine;
    private Coroutine _cooldownCoroutine;
    private string _maskName = "Enemy";

    public event Action<float, float> TimeChanged;

    private void Awake()
    {
        _health = GetComponent<Health>();
        if (_enemyLayer == 0) _enemyLayer = LayerMask.GetMask(_maskName);        
        TimeChanged.Invoke(_duration, _duration);
    }

    public void StartVampirismEffect()
    {
        if (_isActive) return;

        if (_attackingCoroutine != null) StopCoroutine(_attackingCoroutine);
        _attackingCoroutine = StartCoroutine(AttackingRoutine());
    }

    private IEnumerator AttackingRoutine()
    {
        _isActive = true;
        _vampirimsArea.ActivateEffect();

        for (float i = 0; i < _duration; i++)
        {
            Attack();
            TimeChanged?.Invoke(i, _duration);
            yield return _attackInterval;
        }

        _vampirimsArea.DeactivateEffect();
        TimeChanged?.Invoke(_duration, _duration);

        _cooldownCoroutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        for (int i = 0; i < (int)_cooldown; i++)
    {
        TimeChanged?.Invoke(i, _cooldown);
        yield return _oneSecondWait; 
    }

    TimeChanged?.Invoke(_cooldown, _cooldown);
    _isActive = false;
    TimeChanged?.Invoke(_duration, _duration);
    }

    private void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);
        HashSet<IDemagable> targets = new HashSet<IDemagable>();

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent(out IDemagable target))
                targets.Add(target);
        }

        IDemagable closest = CheckDistance(targets);

        if (closest != null)
        {
            float damage = Damage(closest);
            float heal = Mathf.Min(damage, _healAmount);
            
            RestoreHealth(heal);
        }
    }

    private IDemagable CheckDistance(HashSet<IDemagable> targets)
    {
        float minDistance = float.MaxValue;
        IDemagable closestTarget = null;

        foreach (var target in targets)
        {
            if (target is Component targetComponent)
            {
                float sqrDist = (targetComponent.transform.position - transform.position).sqrMagnitude;
                if (sqrDist < minDistance)
                {
                    minDistance = sqrDist;
                    closestTarget = target;
                }
            }
        }
        return closestTarget;
    }

    private void RestoreHealth(float healAmmount)
    {
        _health.Restore(healAmmount);
    } 
        
    private float Damage(IDemagable target)
    {
        return target.TakeDamage(_damageAmount);
    }
}