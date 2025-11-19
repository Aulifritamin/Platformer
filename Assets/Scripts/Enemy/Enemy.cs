using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyPatrol _patrol;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _patrol.Initialize(_rigidbody, transform);
    }

    private void FixedUpdate()
    {
        _patrol.Patrol();
    }

    private void OnDrawGizmos()
    {
        if (_patrol != null)
        {
            _patrol.DrawGizmos();
        }
    }
}
