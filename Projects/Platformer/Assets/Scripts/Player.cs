using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;

    [Header("Movement")] 
    [SerializeField] float _speed = 1;
    [SerializeField] float _slipFactor = 1;
    [SerializeField] float _acceleration = 1;
    [SerializeField] float _breaking = 1;
    [SerializeField] float _airBreaking;
    [SerializeField] float _airAcceleration;

    [Header("Jumping")]
    [SerializeField] float _jumpVelocity = 10;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] float _downPull = 5f;
    [SerializeField] float _wallSlideSpeed = 1f;

    [Header("Sensors")]
    [SerializeField] Transform _feet;
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;

    Rigidbody2D _rb2d;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    Vector2 _startPos;    //to reset player if dead

    int _jumpsRemaining;
    float _fallTimer;
    float _jumpTimer;
    float _horizontal;
    bool _isGrounded;             // ground check
    bool _isOnSlipperySurface;   //slippery check
    string _jumpButton;
    string _horizontalAxis;
    int _layerMask;
    
    AudioSource _audioSource;

    public int PlayerNumber => _playerNumber;

    void Start()
    {
        //caching
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startPos = transform.position;

        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        _layerMask = LayerMask.GetMask("Default");

        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CalculateIsGrounded();

        ReadHorizontalInput();
        if (_isOnSlipperySurface)
            SlipHorizontal();
        else
            MoveHorizontal();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldSlide())
        {
            if (ShouldStartJump())
                WallJump();
            else
                Slide();
            return;
        }

        //jumping functionality 
        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        //jump timer going up every frame
        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = _maxJumps;  //resetting jumps if grounded
        }
        else
        {
            _fallTimer += Time.deltaTime;
            float downForce = _downPull * _fallTimer * _fallTimer;
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _rb2d.velocity.y - downForce);  //pulling down for smooth falling
        }

    }

    void WallJump()
    {
        _rb2d.velocity = new Vector2(-_horizontal * _jumpVelocity, _jumpVelocity *1.5f);
    }

    void Slide()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, -_wallSlideSpeed);  //pulling down for sliding
    }

    bool ShouldSlide()
    {
        if (_isGrounded)
            return false;

        if (_rb2d.velocity.y > 0)
            return false;

        if(_horizontal < 0)
        {
            Collider2D hit = Physics2D.OverlapCircle(_leftSensor.position, 0.1f);
            if(hit != null && hit.CompareTag("Wall"))
                return true;
        }

        if (_horizontal > 0)
        {
            Collider2D hit = Physics2D.OverlapCircle(_rightSensor.position, 0.1f);
            if (hit != null && hit.CompareTag("Wall"))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Jumping lenghts functionality 
    /// </summary>
    void ContinueJump()
    {
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }


    /// <summary>
    /// jumping functionality
    /// </summary>
    void Jump()
    {
        // isGrounded = false;
        _jumpsRemaining--;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpVelocity);
        _fallTimer = 0;
        _jumpTimer = 0;

        if(_audioSource != null)
            _audioSource.Play();

    }

    /// <summary>
    /// jumping check
    /// </summary>
    /// <returns></returns>
    bool ShouldStartJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    /// <summary>
    /// Horizontal checks and moving
    /// </summary>
    void MoveHorizontal()
    {
        float smoothnessMultiplier = _horizontal == 0 ? _breaking : _acceleration;
        if(!_isGrounded)
            smoothnessMultiplier = _horizontal == 0 ? _airBreaking : _airAcceleration;

        float newHorizontal = Mathf.Lerp(_rb2d.velocity.x, _horizontal * _speed, Time.deltaTime * smoothnessMultiplier);
        _rb2d.velocity = new Vector2(newHorizontal, _rb2d.velocity.y);
    }

    void SlipHorizontal()
    {
        Vector2 desiredVelocity = new Vector2(_horizontal * _speed, _rb2d.velocity.y);
        Vector2 smoothedVelocity = Vector2.Lerp(
            _rb2d.velocity,
            desiredVelocity, 
            Time.deltaTime / _slipFactor);

            _rb2d.velocity = smoothedVelocity;
    }

    void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxis) * _speed;
    }

    /// <summary>
    /// updating direction for animation
    /// </summary>
    void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    /// <summary>
    /// Updating animator is player is walking
    /// </summary>
    void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool("Walk", walking);
        _animator.SetBool("Jump", ShouldContinueJump());
        _animator.SetBool("Slide", ShouldSlide());
    }

    /// <summary>
    /// Isgrounded check
    /// </summary>
    /// <returns></returns>
    void CalculateIsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(_feet.position, 0.1f, _layerMask);
        _isGrounded = hit != null;

        if(hit != null)
        {
            _isOnSlipperySurface = hit.CompareTag("Snow");
        }
        else
        {
            _isOnSlipperySurface = false;
        }

        // _isOnSlippertySurface = hit?.CompareTag("Snow") ?? false;

    }

    /// <summary>
    /// Reset the player in case of death
    /// </summary>
    internal void ResetToStart()
    {
        _rb2d.position = _startPos;
    }

    internal void TeleportTo(Vector3 position)
    {
        _rb2d.position = position;
        _rb2d.velocity = Vector2.zero;
    }
}
