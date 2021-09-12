using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;

    Vector2 cornerOne;
    Vector2 cornerTwo;

    Timer spawnTimer;

    private void Start()
    {
        Instantiate(ballPrefab);

        spawnTimer = gameObject.AddComponent<Timer>();
        //settting up timer to spawn ball at random
        spawnTimer.Duration = Random.Range(ConfigurationUtils.minSpawn, ConfigurationUtils.MaxSpawn);
        spawnTimer.Run();

        //getting corners of the collider to check for collision
        cornerOne = new Vector2(ballPrefab.GetComponent<BoxCollider2D>().bounds.center.x + ballPrefab.GetComponent<BoxCollider2D>().bounds.size.x / 2,
                                ballPrefab.GetComponent<BoxCollider2D>().bounds.center.y + ballPrefab.GetComponent<BoxCollider2D>().bounds.size.y / 2);

        cornerTwo = new Vector2(ballPrefab.GetComponent<BoxCollider2D>().bounds.center.x - ballPrefab.GetComponent<BoxCollider2D>().bounds.size.x / 2,
                                ballPrefab.GetComponent<BoxCollider2D>().bounds.center.y - ballPrefab.GetComponent<BoxCollider2D>().bounds.size.y / 2);

        spawnTimer.AddTimerFinishedListener(OnSpawnTimerFinished);

        EventManager.BallCountListener(SpawnNewBall);
        
    }

    private void Update()
    {

    }

    void OnSpawnTimerFinished()
    {
        SpawnNewBall();
        spawnTimer.Duration = Random.Range(ConfigurationUtils.minSpawn, ConfigurationUtils.MaxSpawn);
        spawnTimer.Run();
    }


     void SpawnNewBall()
    {

        if (Physics2D.OverlapArea(cornerOne, cornerTwo) == null) 
        Instantiate(ballPrefab);
    }

}
