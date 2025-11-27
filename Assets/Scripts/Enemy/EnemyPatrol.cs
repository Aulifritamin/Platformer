using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private float _patrolDistance = 5f;

    private Vector2 _startPosition;
    private CharacterRotator _rotator;
    private Transform _transform;
    private int _direction = 1;

    public void Initialize(Transform transform, CharacterRotator rotator)
    {
        _transform = transform;
        _rotator = rotator;
        _startPosition = _transform.position;
    }

    public void Patrol(EnemyMovement enemyMovement)
    {
        enemyMovement.Move(_direction);

        float currentX = _transform.position.x;
        float rightBoundary = _startPosition.x + _patrolDistance;
        float leftBoundary = _startPosition.x - _patrolDistance;


        bool isGroundAhead = !IsGroundAhead();
        bool reachedRightEdge = _direction > 0 && currentX >= rightBoundary;
        bool reachedLeftEdge = _direction < 0 && currentX <= leftBoundary;

        if (isGroundAhead || reachedRightEdge || reachedLeftEdge)
        {
            FlipDirection();
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

        _rotator.Rotate(_direction);
    }

    public void DrawGizmos()
    {
        if (_groundCheck == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Vector3 start = _groundCheck.position;
        Vector3 end = _groundCheck.position + Vector3.down * _groundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
}
