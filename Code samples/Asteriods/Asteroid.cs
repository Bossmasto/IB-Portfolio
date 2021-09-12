using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    /// <summary>
    /// Asteroid behaviour
    /// </summary>

    Rigidbody2D asteroid;

    [SerializeField] List<Sprite> sprites;

    const float MinImpulseForce = 1.5f;
    const float MaxImpulseForce = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        //Getting sprite renderer and applying random sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        int _randomSprite = Random.Range(0, sprites.Count-1);
        spriteRenderer.sprite = sprites[_randomSprite];

        
    }

    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position

        transform.position = position;
        
        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;

        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        StartMoving(angle);
    }

    public void StartMoving(float angle)
    {
        Vector2 movedirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);

        GetComponent<Rigidbody2D>().AddForce(movedirection * magnitude, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //destroying a bullet
            Destroy(collision.gameObject);

            //playing a sound

            AudioManager.Play(AudioClipName.AsteroidHit);

            //splitting asteroids
            if (gameObject.transform.localScale.x < 0.5)
            {
                Destroy(gameObject);
            }
            else
            {

                //splitting asteroids
                Vector3 localScale = gameObject.transform.localScale;
                localScale.x /= 2;
                localScale.y /= 2;
                gameObject.transform.localScale = localScale;


                float newRadius = gameObject.GetComponent<CircleCollider2D>().radius;

                gameObject.GetComponent<CircleCollider2D>().radius = newRadius / 2;

                //spawning 2 asteroids
                GameObject astro1 = Instantiate<GameObject>(gameObject);
                astro1.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));
                GameObject astro2 = Instantiate<GameObject>(gameObject);
                astro2.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));

                //destroying the original one
                Destroy(gameObject);
            }
        }
    }
}

