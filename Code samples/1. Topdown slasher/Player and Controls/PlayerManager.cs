using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    Player[] _players;

    void Awake()
    {
        Instance = this;
        _players = FindObjectsOfType<Player>();    
    }

    public void AddPlayerToGame(Controller controller)
    {
        Player firstNonActivaPlayer = _players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);
        firstNonActivaPlayer.InitializePlayer(controller);
    }

    public void SpawnPlayerCharacters()
    {
        foreach (Player player in _players)
        {
            if (player.HasController && player.CharacterPrefab != null)
                player.SpawnCharacter();
        }
    }
}
