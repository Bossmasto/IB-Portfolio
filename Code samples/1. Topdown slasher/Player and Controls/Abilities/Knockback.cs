using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : AbilityBase, IDamage
{
    [SerializeField] int _damage = 3;
    [SerializeField] float _attackRadius = 2;
    [SerializeField] float _impactDelay = 0.25f;
    [SerializeField] float _forceAmount = 10f;

    Collider[] _attackResults;
    LayerMask _layerMask;

    public int Damage => _damage;

    void Awake()
    {
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        _layerMask = ~LayerMask.GetMask(currentLayer);

        _attackResults = new Collider[10];
    }

    void Attack()
    {
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(_impactDelay);

        Vector3 position = transform.position + transform.forward;
        int hitCount = Physics.OverlapSphereNonAlloc(position, _attackRadius, _attackResults, _layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = _attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
            {
                takeHit.TakeHit(this);
            }

            var hitRigidbody = _attackResults[i].GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                Vector3 direction = Vector3.Normalize(hitRigidbody.transform.position - transform.position);

                hitRigidbody.AddForce(direction * _forceAmount, ForceMode.Impulse);
            }
        }
    }

    protected override void OnUse()
    {
        Attack();
    }
}
