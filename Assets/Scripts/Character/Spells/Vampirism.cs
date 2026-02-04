using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(InputListener)]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _healAmount = 10f;
    [SerializeField] private float _damageAmount = 8f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private SpriteRenderer _vampirimsArea;

    private bool _isActive = false;
    private WaitForSeconds _durationWait = new WaitForSeconds(_duration);

    private InputListener _inputListener;

    private void Awake()
    {
        _inputListener = GetComponent<InputListener>();
        _enemyLayer = LayerMask.GetMask("Enemy");
        _vampirimsArea.enabled = false;
    }

    private IEnumerator AttackingRoutine()
    {
        if (_isActive)
        {
            yield break;
        }

        _isActive = true;
        _vampirimsArea.enabled = true;
        float timer = 0f;

        while(timer < _duration)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        _vampirimsArea.enabled = false;
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        float timer = 0f;
        while (timer < _cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _isActive = false;
    }

    private void Attack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);

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
            target.TakeDamage(_damageAmount);
        }
    }
}
