using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to spawn asteroids
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] GameObject prefabAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        //saving asteroid radious
        GameObject tempAsteroid = Instantiate<GameObject>(prefabAsteroid);
        CircleCollider2D collider = tempAsteroid.GetComponent<CircleCollider2D>();
        float colliderRadius = collider.radius;
        Destroy(tempAsteroid);

        // calculate screen width and height
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        float screenHeight = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;

        //spawning right to left
        GameObject asteroid = Instantiate<GameObject> (prefabAsteroid);
        Asteroid script = asteroid.GetComponent<Asteroid>();


        script.Initialize(Direction.Left, new Vector3(ScreenUtils.ScreenRight + colliderRadius, (ScreenUtils.ScreenBottom + screenHeight)/2));

        //spawning left to right
        asteroid = Instantiate<GameObject>(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();

        script.Initialize(Direction.Right, new Vector3(ScreenUtils.ScreenLeft - colliderRadius, (ScreenUtils.ScreenBottom + screenHeight) / 2));

        //spawning bottom to Top
        asteroid = Instantiate<GameObject>(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();

        script.Initialize(Direction.Up, new Vector3((ScreenUtils.ScreenLeft + screenWidth) / 2, (ScreenUtils.ScreenBottom - colliderRadius)));

        //spawning right to left
        asteroid = Instantiate<GameObject>(prefabAsteroid);
        script = asteroid.GetComponent<Asteroid>();

        script.Initialize(Direction.Down, new Vector3((ScreenUtils.ScreenLeft + screenWidth) / 2, (ScreenUtils.ScreenTop + colliderRadius)));




    }
}

