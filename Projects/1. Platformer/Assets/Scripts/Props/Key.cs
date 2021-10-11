using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock _keyLock;

    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _keyLock = GameObject.FindGameObjectWithTag("BlueLock").GetComponent<KeyLock>();
    }


        void OnTriggerEnter2D(Collider2D collision)
    {
        //Player picks up the key
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
            if (_audioSource != null)
                _audioSource.Play();
        }

        //unlocking the right lock
        KeyLock keyLock = collision.GetComponent<KeyLock>();
        if (keyLock != null && keyLock == _keyLock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
