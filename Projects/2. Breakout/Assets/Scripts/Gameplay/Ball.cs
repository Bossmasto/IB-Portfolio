using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    Rigidbody2D ball;

    Timer timer;
    Timer respawnTimer;
    public float respawnDuration = 2f;
    Vector2 ballDirection;

    //effect fields
    Timer speedTimer;
    float speedupFactor = 1;

    //events for reducing ball count
    ReduceBallCountActivated reduceBallCount;



    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();

        //setting up the timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = ConfigurationUtils.BallLifeTime;
        timer.Run();

        //setting respawnTimer
        respawnTimer = gameObject.AddComponent<Timer>();
        respawnTimer.Duration = respawnDuration;
        respawnTimer.Run();

        //speedup effect work
        speedTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedEffectListener(OnSpeedUp);

        //reduce ball count event
        reduceBallCount = new ReduceBallCountActivated();
        EventManager.BallCountInvoker(this);

        //listen to timers
        respawnTimer.AddTimerFinishedListener(OnRespawnTimerFinished);
        timer.AddTimerFinishedListener(OnTimerFinished);
        speedTimer.AddTimerFinishedListener(OnSpeedTimerFinished);

    }
    //sending the Ball flying in the right direction
    public void SetDirection(Vector2 direction)
    {
        float speed = ball.velocity.magnitude;
        ball.velocity = direction * speed;
    }

    private void Update()
    {

    }

    //going back to normal speedn

    void OnSpeedTimerFinished()
    {
        speedTimer.Stop();
        ball.velocity *= 1 / speedupFactor;
    }

    //destroy balls every x second
    void OnTimerFinished()
    {
        Destroy(gameObject);
        timer.Run();
       // Camera.main.GetComponent<BallSpawner>().SpawnNewBall();
    }

    //respawning every X seconds
    void OnRespawnTimerFinished()
    {
        respawnTimer.Stop();
        StartMoving();
    }

    void StartMoving()
    {
        float angle = -90 * Mathf.Deg2Rad;
        ballDirection = new Vector2(Mathf.Cos(angle) * ConfigurationUtils.BallImpulseForce,
                                            Mathf.Sin(angle) * ConfigurationUtils.BallImpulseForce);
        //adjust speedup effects
        if (EffectUtils.EffectRunning)
        {
            StartSpeedupEffect(3, EffectUtils.SpeedupFactor);
            ballDirection *= speedupFactor;
        }


        //get the ball moving
        ball.AddForce(ballDirection);

    }

    private void OnBecameInvisible()
    {

        float halfColliderHeight =
         gameObject.GetComponent<BoxCollider2D>().size.y / 2;
        if (transform.position.y - halfColliderHeight < ScreenUtils.ScreenBottom && !timer.Finished)
        {
            //invoking reduce ball event
            reduceBallCount.Invoke();
           //Camera.main.GetComponent<BallSpawner>().SpawnNewBall();
        }
        Destroy(ball.gameObject);

    }

    void OnSpeedUp(float duration, float factor)
    {
        if (!speedTimer.Running)
        {
            StartSpeedupEffect(duration, factor);

            Vector2 speed = ball.velocity;
            speed *= 2;
            Ball[] balls = FindObjectsOfType<Ball>();
            foreach (Ball ball in balls)
            {
                ball.GetComponent<Rigidbody2D>().velocity = speed;
            }
        }
        else
        {
            speedTimer.IncreaseDuration(duration);
        }
    }
    void StartSpeedupEffect(float duration, float speedupFactor)
    {
        this.speedupFactor = speedupFactor;
        speedTimer.Duration = duration;
        speedTimer.Run();
    }

    public void ReduceBallCountListener(UnityAction listener)
    {
        reduceBallCount.AddListener(listener);
    }
}

