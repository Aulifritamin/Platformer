using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private CharacterRotator _rotator;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _patrol = GetComponent<EnemyPatrol>();
        _rotator = GetComponent<CharacterRotator>();
    }

    private void Start()
    {
        _patrol.Initialize(transform, _rotator);
        _enemyMovement.Initialize(_rigidbody);
    }

    private void FixedUpdate()
    {
        _patrol.Patrol(_enemyMovement);
    }

    private void OnDrawGizmos()
    {
        if (_patrol != null)
        {
            _patrol.DrawGizmos();
        }
    }
}
