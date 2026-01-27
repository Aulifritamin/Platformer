using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputListener))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterRotator))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour, IDemagable
{
    private Rigidbody2D _rigidbody;
    private InputListener _inputListener;
    private GroundDetector _groundDetector;
    private CharacterAnimator _characterAnimator;
    private CharacterRotator _characterRotator;
    private CharacterMover _playerMovement;
    private Weapon _currentWeapon;
    private Health _health;
    private Inventory _inventory;
    
    private float _nextAttackTime = 0f;
    private float _attackCooldown = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputListener = GetComponent<InputListener>();
        _groundDetector = GetComponent<GroundDetector>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _characterRotator = GetComponent<CharacterRotator>();
        _playerMovement = GetComponent<CharacterMover>();
        _health = GetComponent<Health>();
        _inventory = GetComponent<Inventory>();
        _currentWeapon = GetComponentInChildren<Weapon>();
    }

    public void AE_AttackHit()
    {
        _currentWeapon.AttackHit();
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnEnable()
    {
        _inputListener.JumpPressed += JumpPressed;
        _inputListener.AttackPressed += AttackPressed;
        _inventory.AidKidCollected += AidKidCollected;
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _inputListener.JumpPressed -= JumpPressed;
        _inputListener.AttackPressed -= AttackPressed;
        _health.Died -= Die;
        _inventory.AidKidCollected -= AidKidCollected;
    }    

    private void Update()
    {
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moveDirection = _inputListener.MoveInput.x;
        _playerMovement.Move(moveDirection);
        _characterRotator.Rotate(moveDirection);
    }
    
    private void HandleAnimation()
    {
        var moveDirection = _inputListener.MoveInput.x;
        _characterAnimator.SetSpeed(Mathf.Abs(moveDirection));
        _characterAnimator.UpdateJumpFallAnimation(_groundDetector.IsGrounded, _rigidbody.velocity.y);
    }

    private void JumpPressed()
    {
        if (!_groundDetector.IsGrounded)
        {
            return;
        }
        
        _playerMovement.Jump();
    }

    private void AttackPressed()
    {
        if (Time.time >= _nextAttackTime)
        {
            _characterAnimator.SetAttackTrigger();
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }

    private void AidKidCollected(AidKid aidKid)
    {
        _health.Heal(aidKid.HealAmount);
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
