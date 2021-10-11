using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] float _attackRefreshSpeed = 1.5f;
    [SerializeField] PlayerButton _button;
    [SerializeField] protected string _animationTrigger;

    protected float _attackTimer;
    Controller _controller;
    protected Animator _animator;

    public bool CanAttack => _attackTimer >= _attackRefreshSpeed;

    protected abstract void OnUse();

    public void SetController(Controller controller)
    {
        this._controller = controller;
    }

    void Update()
    {
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        _attackTimer += Time.deltaTime;

        if (ShouldTryUse())
        {
            if (string.IsNullOrEmpty(_animationTrigger) == false)
                _animator.SetTrigger(_animationTrigger);

            OnUse();
        }
    }

    bool ShouldTryUse()
    {
        return _controller != null && CanAttack && _controller.ButtonDown(_button);
    }
}