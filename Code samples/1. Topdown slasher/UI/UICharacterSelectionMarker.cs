using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _markerImage;
    [SerializeField] Image _lockImage;

    UICharacterSelectionMenu _menu;
    
    bool _initializing;
    bool _initialized;

    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn => _player.HasController;

    void Awake()
    {
        _menu = GetComponentInParent<UICharacterSelectionMenu>();
        _markerImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!IsPlayerIn)
            return;

        if (!_initializing)
            StartCoroutine(Initialize());

        if (!_initialized)
            return;

        //Check for player Controls and selection + locking character
        if (!IsLockedIn)
        {
            if (_player.Controller._horizontal > 0.5)
                MoveToCharacterPanel(_menu.RightPanel);
            else if (_player.Controller._horizontal < -0.5)
                MoveToCharacterPanel(_menu.LeftPanel);

            if (_player.Controller._attackPressed)
                StartCoroutine( LockCharacter());
        }
        else
        {
            if (_player.Controller._attackPressed)
                _menu.TryStartGame();
        }
    }

    IEnumerator LockCharacter()
    {
        _lockImage.gameObject.SetActive(true);
        _markerImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        
        IsLockedIn = true;
    }

    void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
        _player.CharacterPrefab = panel.CharacterPrefab;
    }

    IEnumerator Initialize()
    {
        _initializing = true;
        MoveToCharacterPanel(_menu.LeftPanel);

        yield return new WaitForSeconds(0.5f);

        _markerImage.gameObject.SetActive(true);
        _initialized = true;
    }


}
