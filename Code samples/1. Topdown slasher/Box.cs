using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] float _forceAmount = 10f;

    Rigidbody _rigidbody;

    public bool Alive => true;

    public event Action OnHit = delegate { };

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TakeHit(IDamage hitBy)
    {
        Vector3 direction = Vector3.Normalize(transform.position - hitBy.transform.position);

        _rigidbody.AddForce(direction * _forceAmount, ForceMode.Impulse);

        OnHit?.Invoke();
    }
}
