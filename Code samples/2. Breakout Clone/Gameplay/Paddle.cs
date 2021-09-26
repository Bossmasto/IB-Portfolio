using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    Rigidbody2D rb2d;
    float _colliderWidth;
    float _colliderHeights;

    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    //Freezing effect calculations
    bool isFrozen = false;
    Timer freezeTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _colliderWidth = GetComponent<BoxCollider2D>().size.x /2;
        _colliderHeights = GetComponent<BoxCollider2D>().size.y / 2;

        //Freeze effect setup
        freezeTimer = gameObject.AddComponent<Timer>();
        EventManager.AddFreezerEffectListener(OnFreeze);
        freezeTimer.AddTimerFinishedListener(OnFreezeTimerFinish);


    }

    private void FixedUpdate()
    {

        //moving the paddle
        float input = Input.GetAxis("Horizontal");
        if (input != 0 && !isFrozen)
        {
            Vector2 position = rb2d.transform.position;
            position.x += input * ConfigurationUtils.PaddleMoveUnitsPerSecond *
                 Time.deltaTime;
            position.x = CalculateClampedX(position.x);
            rb2d.MovePosition(position);
        }
    }

    private void Update()
    {

    }

    void OnFreezeTimerFinish()
    {
        isFrozen = false;
        freezeTimer.Stop();
    }
    float CalculateClampedX(float x)
    {
        // clamp left and right edges
        if (x - _colliderWidth < ScreenUtils.ScreenLeft)
        {
            x = ScreenUtils.ScreenLeft + _colliderWidth;
        }
        else if (x + _colliderWidth > ScreenUtils.ScreenRight)
        {
            x = ScreenUtils.ScreenRight - _colliderWidth;
        }
        return x;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") && IsTop(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                _colliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    bool IsTop(Collision2D coll)
    {
        const float tolerance = 0.05f;

        // on top collisions, both contact points are at the same y location
        ContactPoint2D[] contacts = coll.contacts;
        return Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tolerance;
    }

    void OnFreeze(float duration)
    {
        isFrozen = true;
        if (!freezeTimer.Running)
        {
            freezeTimer.Duration = duration;
            freezeTimer.Run();
        }
        else
        {
            freezeTimer.IncreaseDuration(duration);
        }
    }
}
