using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : PooledMonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] int _damage = 1;
    [SerializeField] int _maxHealth = 10;

    Controller _controller;
    Rigidbody _rb;

    IAttack _attacker;
    Animator _animator;
    int _currentHealth;

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<IDie> OnDied = delegate { };
    public event Action OnHit = delegate { };

    public int Damage => _damage;

    public bool Alive { get; private set; }

    void Awake()
    {
        _attacker = GetComponent<IAttack>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void SetController(Controller controller)
    {
        this._controller = controller;
        foreach (var ability in GetComponents<AbilityBase>())
        {
            ability.SetController(controller);
        }
    }

    void Update()
    {
        Vector3 direction = _controller.GetDirection();  
        if(direction.magnitude > 0.1f)
        {
            Vector3 velocity = (direction * _moveSpeed).With(y:_rb.velocity.y) ;
            _rb.velocity = velocity;
            transform.forward = direction * 360f;                           //to transition movement into degrees and apply rotation

            _animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
    void OnEnable()
    {
        Alive = true;

        if (!All.Contains(this))
            All.Add(this);

        _currentHealth = _maxHealth;
    }
    protected override void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);

        base.OnDisable();
    }

    public void TakeHit(IDamage hitBy)
    {
        if (_currentHealth <= 0)
            return;

        ModifyHealth(-hitBy.Damage);
        if (_currentHealth <= 0)
            Die();

        OnHit?.Invoke();
    }
    public void Heal(int healAmount)
    {
        ModifyHealth(healAmount);

    }

    void ModifyHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        OnHealthChanged(_currentHealth, _maxHealth);
    }

    void Die()
    {
        Alive = false;
        OnDied(this);
    }

}