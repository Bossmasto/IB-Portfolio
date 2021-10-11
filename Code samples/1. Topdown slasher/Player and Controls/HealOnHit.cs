using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHit : MonoBehaviour, ITakeHit
{
    [SerializeField] int _healAmount = 5;
    [SerializeField] bool _disableOnUse = true;

    public bool Alive => true;

    public event Action OnHit;

    public void TakeHit(IDamage hitBy)
    {
        Character character = hitBy.transform.GetComponent<Character>();
        if(character != null)
        {
            OnHit?.Invoke();
            character.Heal(_healAmount);

            if (_disableOnUse)
                gameObject.SetActive(false);
        }
    }
}
