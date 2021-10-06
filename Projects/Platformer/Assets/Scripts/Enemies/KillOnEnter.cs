using UnityEngine;

public class KillOnEnter : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        player?.ResetToStart();
    }

    void OnParticleCollision(GameObject other)
    {
        Player player = other.GetComponent<Player>();
        player?.ResetToStart();
    }
}

