using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.Center;

    [SerializeField] Sprite _leftSwitch;
    [SerializeField] Sprite _rightSwitch;
    [SerializeField] Sprite _centerSwitch;

    [SerializeField] UnityEvent _onLeftSwitch;
    [SerializeField] UnityEvent _onRightSwitch;
    [SerializeField] UnityEvent _onCentreSwitch;

    [SerializeField] AudioClip _leftSound; 
    [SerializeField] AudioClip _rightSound; 

    SpriteRenderer _spriteRenderer;
    ToggleDirection _currentDirection;

    AudioSource _audioSource;

    enum ToggleDirection
    { 
        Left,
        Center,
        Right,
    }


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(_startingDirection, true);

        _audioSource = GetComponent<AudioSource>();
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        Rigidbody2D playerRB2D = player.GetComponent<Rigidbody2D>();
        if (playerRB2D == null)
            return;

        bool wasOnRight = collision.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRB2D.velocity.x > 0;
        bool playerWalkingLeft = playerRB2D.velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
            SetToggleDirection(ToggleDirection.Right);
        else if (!wasOnRight && playerWalkingLeft)
            SetToggleDirection(ToggleDirection.Left) ;
    }
    
    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        if (force == false && _currentDirection == direction)
            return;

        _currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                _onLeftSwitch?.Invoke();
                _spriteRenderer.sprite = _leftSwitch;

                if(_audioSource != null)
                    _audioSource.PlayOneShot(_leftSound);
                break;

            case ToggleDirection.Center:
                _onCentreSwitch?.Invoke();
                _spriteRenderer.sprite = _centerSwitch;
                break;

            case ToggleDirection.Right:
                _onRightSwitch?.Invoke();
                _spriteRenderer.sprite = _rightSwitch;

                if (_audioSource != null)
                    _audioSource.PlayOneShot(_rightSound);
                break;
            default:
                break;
        }
    }
}
