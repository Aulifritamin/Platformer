using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotator))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundCheck;
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private float _patrolDistance = 5f;

    private Vector2 _startPosition;
    private CharacterRotator _rotator;
    private Transform _enemyTransform;
    private int _direction = 1;
    private bool _isTurning;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
        _groundCheck = GetComponent<GroundDetector>();
        _rotator = GetComponent<CharacterRotator>();
    }

    public void Initialize(Transform transform)
    {
        _enemyTransform = transform;
        _startPosition = _enemyTransform.position;
    }

    public void Patrol()
    {
        if (_groundCheck.IsGrounded)
        {
            _isTurning = false;
        }
        
        _mover.Move(_direction);
        _rotator.Rotate(_direction);

        float currentX = _enemyTransform.position.x;
        float rightBoundary = _startPosition.x + _patrolDistance;
        float leftBoundary = _startPosition.x - _patrolDistance;
        
        bool reachedRightEdge = _direction > 0 && currentX >= rightBoundary;
        bool reachedLeftEdge = _direction < 0 && currentX <= leftBoundary;

        if (_isTurning == false && (_groundCheck.IsGrounded == false || reachedRightEdge || reachedLeftEdge))
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        _direction *= -1;
        _isTurning = true;
    }
}
