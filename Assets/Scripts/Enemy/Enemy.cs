using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyFollow))]
[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IDemagable
{
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private EnemyFollow _follow;
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private Health _health;

    private Transform _target;
    private WaitForSeconds _checkVisionWait = new WaitForSeconds(0.2f);
    private WaitForSeconds _targetLostWait = new WaitForSeconds(4f);
    private Coroutine _targetLostCoroutine;

    private void Awake()
    {
        _patrol = GetComponent<EnemyPatrol>();
        _follow = GetComponent<EnemyFollow>();
        _enemyVision = GetComponent<EnemyVision>();
        _health = GetComponent<Health>();

        StartCoroutine(VisionRoutine());
    }

    private void OnEnable()
    {
        _health.Die += Die;
    }

    private void OnDisable()
    {
        _health.Die -= Die;
    }

    private void FixedUpdate()
    {
        MovementStrategy();
    }

    private void Start()
    {
        _patrol.Initialize(transform);
    }
    
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void MovementStrategy()
    {
        if (_target != null)
        {
            _follow.FollowTarget(_target);
        }
        else
        {
            _patrol.Patrol();
        }
    }

    private IEnumerator VisionRoutine()
    {
        while (enabled)
        {
            Transform visibleTarget = _enemyVision.CheckVision();

            if (visibleTarget != null)
            {
                _target = visibleTarget;

                if (_targetLostCoroutine != null)
                {
                    StopCoroutine(_targetLostCoroutine);
                    _targetLostCoroutine = null;
                }
            }
            else
            {
                if (_target != null && _targetLostCoroutine == null)
                {
                    _targetLostCoroutine = StartCoroutine(TargetLostRoutine());
                }
            }

            yield return _checkVisionWait;
        }
    }

    private IEnumerator TargetLostRoutine()
    {
        yield return _targetLostWait;
        _target = null;
        _targetLostCoroutine = null;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
