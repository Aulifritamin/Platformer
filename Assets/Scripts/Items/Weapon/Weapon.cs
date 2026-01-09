using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRadius = 0.4f;
    [SerializeField] private LayerMask _targetMask;

    private void DealDamage(IDemagable target)
    {
        if (target != null)
        {
            target.TakeDamage(_attackDamage);
        }
    }

    public void AttackHit()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_hitPoint.position, _attackRadius, _targetMask);

        HashSet<IDemagable> damagedTargets = new HashSet<IDemagable>();

        foreach (Collider2D collider in hitColliders)
        {
            if(collider.TryGetComponent(out IDemagable target))
            {
                damagedTargets.Add(target);
            }
        }

        foreach (IDemagable target in damagedTargets)
        {
            DealDamage(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_hitPoint == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitPoint.position, _attackRadius);
    }
}