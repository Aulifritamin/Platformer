using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Fall = Animator.StringToHash("Fall");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void SetJumping(bool isJumping)
    {
        _animator.SetBool(Jump, isJumping);
    }

    public void SetFalling(bool isFalling)
    {
        _animator.SetBool(Fall, isFalling);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger("Attack");
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
