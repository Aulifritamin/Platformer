using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly int _speed = Animator.StringToHash("Speed");
    private static readonly int _jump = Animator.StringToHash("Jump");
    private static readonly int _fall = Animator.StringToHash("Fall");
    private static readonly int _attack = Animator.StringToHash("Attack");
    private static readonly int _vampirism = Animator.StringToHash("Vampirism");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(_speed, speed);
    }

    public void SetJumping(bool isJumping)
    {
        _animator.SetBool(_jump, isJumping);
    }

    public void SetFalling(bool isFalling)
    {
        _animator.SetBool(_fall, isFalling);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(_attack);
    }

    public void SetVampirismActive(bool isActive)
    {
        _animator.SetBool(_vampirism, isActive);
    }

    public void UpdateJumpFallAnimation(bool isGrounded, float verticalVelocity)
    {
        if (isGrounded)
        {
            SetJumping(false);
            SetFalling(false);
            return;
        }

        if (verticalVelocity > 0.01f)
        {
            SetJumping(true);
            SetFalling(false);
        }
        else if (verticalVelocity < -0.01f)
        {
            SetJumping(false);
            SetFalling(true);
        }
    }
}
