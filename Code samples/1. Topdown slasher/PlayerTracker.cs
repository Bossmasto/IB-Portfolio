using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    CinemachineTargetGroup _targetGroup;

    void Awake()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();

        Player[] players = FindObjectsOfType<Player>();

        foreach(Player player in players)
        {
            player.OnCharacterChanged += (character) => Player_OnCharacterChanged(player,character) ;
        }
    }

    private void Player_OnCharacterChanged(Player player, Character character)
    {
        int playerIndex = player.PlayerNumber - 1;
        _targetGroup.m_Targets[playerIndex].target = character.transform;
    }
}
