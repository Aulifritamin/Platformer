using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputListener))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    private InputListener _inputListener;
    private Rigidbody2D _rigidbody;
    private float _groundCheckRadius = 0.15f;
    private float _moveSpeed = 5f;
    private float _jumpForce = 10f;
    private bool isGround = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputListener = GetComponent<InputListener>();
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
        UpdateGround();
        UpdateJumpFallAnim();
    }

    private void FixedUpdate()
    {
        Vector2 movement = _inputListener.MoveInput.normalized;
        _rigidbody.velocity = new Vector2(movement.x * _moveSpeed, _rigidbody.velocity.y);

        _animator?.SetFloat("Speed", Mathf.Abs(movement.x));

        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void UpdateGround()
{
    isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);
}

    public void JumpEvent()
    {
        if (!isGround)
        {
            return;
        }

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void UpdateJumpFallAnim()
    {
        if (isGround)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", false);
            return;
        }

        if (_rigidbody.velocity.y > 0.01f)
        {

            _animator.SetBool("Jump", true);
            _animator.SetBool("Fall", false);
        }
        else if (_rigidbody.velocity.y < -0.01f)
        {

            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", true);
        }
    }
}
