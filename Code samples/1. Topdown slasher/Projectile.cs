using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledMonoBehaviour, IDamage
{
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] int _damage = 1;
    [SerializeField] PooledMonoBehaviour _impactParticlePrefab;

    public int Damage => _damage;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.collider.GetComponent<ITakeHit>();
        if (hit != null)
            Impact(hit);
        else
        {
            _impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
            ReturnToPool();
        }
    }

    void Impact(ITakeHit hit)
    {
        _impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
        hit.TakeHit(this);

        ReturnToPool();
    }
}
