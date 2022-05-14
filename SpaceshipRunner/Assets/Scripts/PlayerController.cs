using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _acceleration = 0.005f;
    [SerializeField] private float _maximumSpeedMultiplier = 1.5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _allowedJumpTime = 10f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private Animator _animator;

    private Vector2 _velocity = default;
    private bool _hasJumpInput = false;
    private float _jumpTime = 0f;
    private float _multiplier = 1f;
    private bool _isJumping = false;
    private bool _isGrounded = true;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AssignPreFrameValues();
        GatherInput();

        CalculateRun();
        CalculateGravity();
        CalculateJump();

        SetAnimations();
        ExecuteMovements();

        AcceleratePlayer();
    }
    private void AssignPreFrameValues()
    {
        _isGrounded = IsGrounded();
    }
    private void GatherInput()
    {
        _hasJumpInput = Input.GetKey(KeyCode.Space);
    }
    private void CalculateRun()
    {
        _velocity.x = _speed * _multiplier;
    }
    private void CalculateGravity()
    {
        if(!_isGrounded)
        {
            _velocity.y -= _gravity * Time.deltaTime * _multiplier;

            RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down, _velocity.y, _groundMask);
            if(raycastHit.collider != null)
            {
                _velocity.y = raycastHit.distance;
            }
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
            _velocity.y = _jumpForce * _multiplier;
        }
        else if(!_isGrounded && _hasJumpInput && _jumpTime < _allowedJumpTime && _isJumping)
        {
            _jumpTime += Time.deltaTime * _multiplier;
            _velocity.y = _jumpForce * _multiplier;
        }
        else if(!_isGrounded && !_hasJumpInput)
        {
            _isJumping = false;
        }
    }
    private void SetAnimations()
    {
        _animator.SetBool("IsGrounded", _isGrounded);
    }
    private void ExecuteMovements()
    {
        _rigidbody.velocity = _velocity;
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0, Vector2.down, 0.1f, _groundMask);
        return raycastHit.collider;
    }

    private void AcceleratePlayer()
    {
        if(_multiplier < _maximumSpeedMultiplier)
        {
            _multiplier += _acceleration * Time.deltaTime;
        }
    }
}
