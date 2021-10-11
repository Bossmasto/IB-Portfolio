using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    string _attackButton;
    string _specialAttackButton;
    string _jumpButton;
    string _horizontalAxis;
    string _verticalAxis;

    public bool _attack;
    public bool _attackPressed;
    bool _specialAttackPressed;
    bool _jumpPressed;

    public float _horizontal;
    public float _vertical;

    int _index;
    public int Index { get => _index; private set => _index = value; }
    public bool IsAssigned { get; set; }

    void Update()
    {
        if (!string.IsNullOrEmpty(_attackButton))
        {
            _attack = Input.GetButton(_attackButton);
            _attackPressed = Input.GetButtonDown(_attackButton);
            _specialAttackPressed = Input.GetButtonDown(_specialAttackButton);
            _jumpPressed = Input.GetButtonDown(_jumpButton);
            _horizontal = Input.GetAxis(_horizontalAxis);
            _vertical = Input.GetAxis(_verticalAxis);
        }
    }

    internal bool ButtonDown(PlayerButton button)
    {
        switch (button)
        {
            case PlayerButton.Attack:
                return _attackPressed;
            case PlayerButton.SpecialAttack:
                return _specialAttackPressed;
            case PlayerButton.Jump:
                return _jumpPressed;
            default:
                return false;
        }
    }

    public void SetIndex(int index)
    {
        Index = index;
        _attackButton = $"Attack{Index}";
        _specialAttackButton = $"SpecialAttack{Index}";
        _jumpButton = $"Jump{Index}";
        _horizontalAxis = $"Horizontal{Index}";
        _verticalAxis = $"Vertical{Index}";

        gameObject.name = $"Controller{Index}";
    }

    internal bool AnyButtonDown()
    {
        return _attack;
    }
    public Vector3 GetDirection()
    {
        return new Vector3(_horizontal, 0, _vertical);
    }
}
