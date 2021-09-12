using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Behaviour and flying bullets
/// </summary>

public class Bullet : MonoBehaviour
{
    //life and death of bullets
    const float lifeTime = 2;
    Timer timer;



    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent <Timer>() as Timer;
        timer.Duration= lifeTime;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            Destroy(gameObject);
        }
    }

    //Setting up a direction for the bullets
    public void ApplyForce(Vector2 direction)
    {
        const float magnitude = 5f;
        GetComponent<Rigidbody2D>().AddForce(magnitude * direction, ForceMode2D.Impulse);
    }
}
