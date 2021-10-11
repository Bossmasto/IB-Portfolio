using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber;

    UIPlayerText _uiPlayerText;

    public event Action<Character> OnCharacterChanged = delegate { };


    public Controller Controller { get; private set; }
    public bool HasController => Controller != null;
    public int PlayerNumber => _playerNumber;
    public Character CharacterPrefab { get; set; }

    void Awake()
    {
        _uiPlayerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitializePlayer(Controller controller)
    {
        Controller = controller;

        gameObject.name = $"Player {controller.gameObject.name}";

        _uiPlayerText.HandlePlayerInitialized();
    }

    public void SpawnCharacter()
    {
        Character character = CharacterPrefab.Get<Character>(Vector3.zero, Quaternion.identity);
        character.SetController(Controller);
        character.OnDied += Character_OnDied;

        OnCharacterChanged(character);
    }

    void Character_OnDied(IDie character)
    {
        character.OnDied -= Character_OnDied;

        character.gameObject.SetActive(false);

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(5);
        SpawnCharacter();
    }
}
