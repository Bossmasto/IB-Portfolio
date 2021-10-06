using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    [SerializeField] List<AudioClip> _clips;

    /// <summary>
    /// Coin pickup method
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        CoinsCollected++;

        ScoreSystem.Add(100);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        if (_clips.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _clips.Count);
            AudioClip clip = _clips[randomIndex];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
