using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private float _patrolDistance = 5f;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody;
    private int _direction = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        float currentX = transform.position.x;
        _rigidbody.velocity = new Vector2(_direction * _moveSpeed, _rigidbody.velocity.y);

        if (!IsGroundAhead())
        {
            FlipDirection();
            return;
        }

        if (_direction > 0 && currentX >= _startPosition.x + _patrolDistance)
        {
            FlipDirection();
            return;
        }

        if (_direction < 0 && currentX <= _startPosition.x - _patrolDistance)
        {
            FlipDirection();
            return;
        }
    }

    private bool IsGroundAhead()
    {
        return Physics2D.Raycast(
            _groundCheck.position,
            Vector2.down,
            _groundCheckDistance,
            _groundLayer
        );
    }

    private void FlipDirection()
    {
        _direction *= -1;

        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 start = _groundCheck.position;
        Vector3 end = _groundCheck.position + Vector3.down * _groundCheckDistance;

        Gizmos.DrawLine(start, end);
    }
}
