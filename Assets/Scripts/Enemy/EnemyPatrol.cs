using UnityEngine;

[System.Serializable]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private float _patrolDistance = 5f;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private int _direction = 1;

    public void Initialize(Rigidbody2D rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
        _startPosition = _transform.position;
    }

    public void Patrol()
    {
        float currentX = _transform.position.x;
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

        Vector3 scale = _transform.localScale;
        scale.x = -scale.x;
        _transform.localScale = scale;
    }

    public void DrawGizmos()
    {
        if (_groundCheck == null) return;
        Gizmos.color = Color.red;
        Vector3 start = _groundCheck.position;
        Vector3 end = _groundCheck.position + Vector3.down * _groundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
}
