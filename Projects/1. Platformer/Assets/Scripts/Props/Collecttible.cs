using System;
using System.Collections.Generic;
using UnityEngine;

public class Collecttible : MonoBehaviour 
{
    public event Action OnPickedUp;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        OnPickedUp?.Invoke();

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
    }
}