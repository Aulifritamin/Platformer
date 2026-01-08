using System.Collections;
using UnityEngine;

public class CheckRange : MonoBehaviour
{
    [SerializeField] private float _minDistanceToTarget = 0.2f;

    private float _sqrMinDistanceToTarget;
    private WaitForSeconds _wait = new WaitForSeconds(0.05f);
    private Coroutine _activeRoutine; 
    
    public bool IsInRange { get; private set; } = false;

    private void Awake()
    {
        _sqrMinDistanceToTarget = _minDistanceToTarget * _minDistanceToTarget;    
    }

    public void SetTarget(Transform target)
    {
        if (_activeRoutine != null)
        {
            StopCoroutine(_activeRoutine);
            _activeRoutine = null;
        }

        if (target != null)
        {
            _activeRoutine = StartCoroutine(TargetCheckRoutine(target));
        }
        else
        {
            IsInRange = false;
        }
    }

    private IEnumerator TargetCheckRoutine(Transform target)
    {
        while (target != null)
        {
            IsInRange = IsEnoughClose(transform.position, target.position);
            yield return _wait;
        }

        IsInRange = false;
        _activeRoutine = null;
    }

    private bool IsEnoughClose(Vector3 start, Vector3 end)
    {
        return (end - start).sqrMagnitude <= _sqrMinDistanceToTarget;
    }
}