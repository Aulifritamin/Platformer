using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputListener))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterRotator))]
public class PlayerMovement : MonoBehaviour
{
    private InputListener _inputListener;
    private Rigidbody2D _rigidbody;
    private GroundDetector _groundDetector;
    private CharacterAnimator _characterAnimator;
    private CharacterRotator _characterRotator;

    private float _moveSpeed = 5f;
    private float _jumpForce = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputListener = GetComponent<InputListener>();
        _groundDetector = GetComponent<GroundDetector>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _characterRotator = GetComponent<CharacterRotator>();
    }

    private void OnEnable()
    {
        _inputListener.JumpPressed += JumpEvent;
    }
    private void OnDisable()
    {
        _inputListener.JumpPressed -= JumpEvent;
    }

    private void Update()
    {
        _characterAnimator.UpdateJumpFallAnimation(_groundDetector.IsGrounded, _rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        Vector2 movement = _inputListener.MoveInput.normalized;
        _rigidbody.velocity = new Vector2(movement.x * _moveSpeed, _rigidbody.velocity.y);

        _characterAnimator.SetSpeed(Mathf.Abs(movement.x));
        _characterRotator.Rotate(movement.x);
    }

    public void JumpEvent()
    {
        if (!_groundDetector.IsGrounded)
        {
            return;
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}
