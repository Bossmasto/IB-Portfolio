using System;
using System.Collections;
using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    [SerializeField] int _damage = 1;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float _launchYOffset = 1;
    [SerializeField] float _launchDelay = 1f;

    public int Damage => _damage;
    public void Attack()
    {
        StartCoroutine(LaunchAfterDelay());
    }

    IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(_launchDelay);
        projectilePrefab.Get<Projectile>(transform.position + Vector3.up * _launchYOffset, transform.rotation);
    }

    protected override void OnUse()
    {
        Attack();
    }
}
