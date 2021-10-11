using System;
using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;
    
    Animator _animator;
    AudioSource _audioSource;

    protected virtual bool CanUse => true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CanUse)
            return;

        Player player = collision.collider.GetComponent<Player>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            PlayAnimation();
            PlayAudio();
            Use();

            if (!CanUse)
                GetComponent<SpriteRenderer>().sprite = _usedSprite;
        }
    }

    void PlayAudio()
    {
        if (_audioSource != null)
            _audioSource.Play();
    }

    void PlayAnimation()
    {
        _animator?.SetTrigger("Use");
    }

    protected abstract void Use();
}
