using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10f;

    AudioSource _audioSource;

    void Awake() => _audioSource = GetComponent<AudioSource>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>(); 
        if(player != null)
        {
            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            if(rb2d != null)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, _bounceVelocity);
                if (_audioSource != null)
                    _audioSource.Play();
            }
        }
    }
}