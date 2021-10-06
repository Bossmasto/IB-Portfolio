using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{

    [SerializeField] List<Collecttible> _collecttibles;
    [SerializeField] UnityEvent _onCollectionComplete;

    TMP_Text _remainingText;
    int _countCollected;

    void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach (Collecttible collecttible in _collecttibles)
        {
            collecttible.OnPickedUp += ItemPickedUp;
        }

        int countRemaining = _collecttibles.Count - _countCollected;
        _remainingText?.SetText(countRemaining.ToString());
    }

    public void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collecttibles.Count - _countCollected;
        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0)
            return;

        _onCollectionComplete.Invoke();
    }

    void OnValidate()
    {
        _collecttibles = _collecttibles.Distinct().ToList();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var collectible in _collecttibles)
        {
            Gizmos.DrawLine(transform.position,collectible.transform.position);
        }    
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in _collecttibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
}
