using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{

    [SerializeField] int _maxHealth = 3;
    [SerializeField] int _damage = 1;

    int _currentHealth;
    Animator _animator;
    
    NavMeshAgent _navMeshAgent;
    Character _target;

    Attacker _attacker;

    public event System.Action<IDie> OnDied = delegate { };
    public event System.Action<int, int> OnHealthChanged = delegate { };
    public event System.Action OnHit = delegate { };

    bool IsDead => _currentHealth <= 0;
    public bool Alive { get; private set; }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _attacker = GetComponent<Attacker>();
    }

    void OnEnable()
    {
        Alive = true;
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (IsDead)
            return;

        if(_target == null || _target.Alive == false)
            AcquireTarget();
        else
        {
            if(_attacker.InAttackRange(_target) == false)
                FollowTarget();
            else
                TryAttack();
        }
    }

    private void AcquireTarget()
    {
        _target = Character.All
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
        _animator.SetFloat("Speed", 0f);
    }

    private void FollowTarget()
    {
        _animator.SetFloat("Speed", 1f);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_target.transform.position);
    }

    private void TryAttack()
    {
        _animator.SetFloat("Speed", 0f);
        _navMeshAgent.isStopped = true;

        if (_attacker.CanAttack)
        {
            _attacker.Attack(_target);
        }
    }

    public void TakeHit(IDamage hitBy)
    {
        _currentHealth-= hitBy.Damage;

        OnHealthChanged(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
             Die();
        else
            _animator.SetTrigger("Hit");

        OnHit?.Invoke();
    }

    void Die()
    {
        _animator.SetTrigger("Die");
        _navMeshAgent.isStopped = true;
        
        Alive = false;
        OnDied(this);

        ReturnToPool(6f);
    }
}
