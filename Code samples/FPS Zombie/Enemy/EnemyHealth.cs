using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] float hitPoints = 10;

    bool isDead = false;

    public bool IsDead { get { return isDead; } }

    //reduce HP by the amount of damage

    public void TakeDamage (float damage)
    {
        BroadcastMessage("OnDamageTaken");


        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
