using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 0.005f;
    [SerializeField] private float _maximumSpeedMultiplier = 1.5f;
    [SerializeField] private float _groundedDistance = 0.05f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _allowedJumpTime = 10f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private Animator _animator;

    private bool _hasJumpInput = false;
    private bool _hasFlipInput = false;

    private Vector2 _velocity = default;
    private float _jumpTime = 0f;
    private float _multiplier = 1f;
    private bool _isJumping = false;
    private bool _isGrounded = true;
    
    private Vector3 _flipPosition = default;
    private Quaternion _flipRotation = default;
    private bool _isFlipped = false;
    private int _flipMultiplier = 1;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AssignPreFrameValues();

        CalculateRun();
        CalculateGravity();
        CalculateJump();
        CalculateFlip();

        SetAnimations();
        ExecuteMovements();

        AcceleratePlayer();
    }
    private void AssignPreFrameValues()
    {
        _isGrounded = IsGrounded();
        _hasJumpInput = InputManager.Instance.IsKeyHolded("Jump");
        _hasFlipInput = InputManager.Instance.IsKeyPressed("Flip");
    }
    private void CalculateRun()
    {
        _velocity.x = _speed * _multiplier;
    }
    private void CalculateGravity()
    {
        if(!_isGrounded)
        {
            _velocity.y -= _gravity * Time.deltaTime * _multiplier * _flipMultiplier;
        }
        else
        {
            _velocity.y = 0;
        }
    }

    private void CalculateJump()
    {
        if(_isGrounded && _hasJumpInput)
        {
            _isJumping = true;
            _jumpTime = 0f;
            _velocity.y = _jumpForce * _multiplier * _flipMultiplier;
        }
        else if(!_isGrounded && _hasJumpInput && _jumpTime < _allowedJumpTime && _isJumping)
        {
            _jumpTime += Time.deltaTime * _multiplier;
            _velocity.y = _jumpForce * _multiplier * _flipMultiplier;
        }
        else if(!_isGrounded && !_hasJumpInput)
        {
            _isJumping = false;
        }
    }
    private void CalculateFlip()
    {
        if (_hasFlipInput && _isGrounded && !_isJumping) 
        {
            CalculateFlipPosition();
            CalculateFlipRotation();
            _isFlipped = !_isFlipped;
            _flipMultiplier *= -1;
        }
    }
    private void CalculateFlipPosition()
    {
        var groundRayCast = GetGround(_groundedDistance);
        float newYPositionShift = (_collider.bounds.size.y + groundRayCast.collider.bounds.size.y) * -_flipMultiplier;
        _flipPosition = transform.position;
        _flipPosition.y += newYPositionShift;
    }

    private void CalculateFlipRotation()
    {
        _flipRotation = transform.rotation;
        _flipRotation.x = 180f * (_isFlipped ? 0 : 1);
    }

    private void SetAnimations()
    {
        _animator.SetBool("IsGrounded", _isGrounded);
    }
    private void ExecuteMovements()
    {
        if (_hasFlipInput && _isGrounded && !_isJumping)
        {
            transform.position = _flipPosition;
            transform.rotation = _flipRotation;
        }

        _rigidbody.velocity = _velocity;
    }
    private void AcceleratePlayer()
    {
        if (_multiplier < _maximumSpeedMultiplier)
        {
            _multiplier += _acceleration * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return GetGround(_groundedDistance).collider;
    }

    private RaycastHit2D GetGround(float distance)
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, (_isFlipped ? Vector2.up : Vector2.down), distance, _groundMask);
    }
}
