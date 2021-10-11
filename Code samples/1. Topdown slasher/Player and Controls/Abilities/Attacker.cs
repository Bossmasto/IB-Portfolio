using System;
using System.Collections;
using UnityEngine;

public class Attacker : AbilityBase, IAttack
{
    [SerializeField] int _damage = 1;

    [SerializeField] float _attackOffset = 1;
    [SerializeField] float _attackRadius = 1f;

    [SerializeField] float _attackImpactDelay = 1f;
    [SerializeField] float _attackRange = 2;

    LayerMask _layerMask;
    Collider[] _attackResults;

    public int Damage => _damage;

    private void Awake()
    {
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        _layerMask = ~LayerMask.GetMask(currentLayer);

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
        {
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
        }

        _attackResults = new Collider[10];
    }

    public void Attack(ITakeHit target)
    {

        _attackTimer = 0;
        StartCoroutine(DoAttack(target));
    }
    public bool InAttackRange(ITakeHit target)
    {
        if (!target.Alive)
            return false;

        var distance = Vector3.Distance(transform.position, target.transform.position);
        return distance < _attackRange;
    }

    IEnumerator DoAttack(ITakeHit target)
    {
        _animator.SetTrigger(_animationTrigger);
        yield return new WaitForSeconds(_attackImpactDelay);

        if (target.Alive && InAttackRange(target))
            target.TakeHit(this);
    }

    /// <summary>
    /// Called by Animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * _attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, _attackRadius, _attackResults, _layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = _attackResults[i].GetComponent<ITakeHit>();
            takeHit?.TakeHit(this);
        }
    }

    public void Attack()
    {
        _animator.SetTrigger(_animationTrigger);
    }

    protected override void OnUse()
    {
        Attack();
    }
}