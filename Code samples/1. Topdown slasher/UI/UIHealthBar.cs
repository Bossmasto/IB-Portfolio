using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] Image foregroundImage;

    Character _currentCharacter;

    void Awake()
    {
        Player player = GetComponentInParent<Player>();
        player.OnCharacterChanged += Player_OnCharacterChanged;
        gameObject.SetActive(false);
    }

    void Player_OnCharacterChanged(Character character)
    {
        _currentCharacter = character;
        _currentCharacter.OnHealthChanged += HandleHealthChanged;
        _currentCharacter.OnDied += _currentCharacter_OnDied;
        gameObject.SetActive(true);
    }

    void _currentCharacter_OnDied(IDie character)
    {
        character.OnHealthChanged -= HandleHealthChanged;
        character.OnDied -= _currentCharacter_OnDied;
        _currentCharacter = null;
        gameObject.SetActive(false);
    }

    void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        float pct = (float)currentHealth / (float)maxHealth;
        foregroundImage.fillAmount = pct;
    }
}
