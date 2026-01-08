using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] Transform _hitPoint;
    private protected float _attackDamage = 10f;
    private protected float _attackRadius = 0.4f;
    private protected LayerMask _playerMask;

    protected virtual void DealDamage(IDemagable target)
    {
        if (target != null)
        {
            target.TakeDamage(_attackDamage);
        }
    }

    public void AttackHit()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_hitPoint.position, _attackRadius, _playerMask);

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitPoint.position, _attackRadius);
    }
}
