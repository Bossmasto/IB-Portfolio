using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //score
    static  Text scoreText;
    int score = 0;

    //balls left

    static Text ballsLeft;
    int balls = 5;

    LastBallLost lastBallLost = new LastBallLost();

    public int Score
    {
        get { return score; }
    }


    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        scoreText.text = "Current score is : 0";     
        
        ballsLeft = GameObject.FindGameObjectWithTag("BallsLeftText").GetComponent<Text>();
        ballsLeft.text = "Balls left - " + balls.ToString();

        //managing score through events
        EventManager.AddPointsListener(AddScore);
        EventManager.BallCountListener(ReduceBallCount);
        EventManager.AddLastBallLostInvoker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void AddScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Current score is : " + score.ToString();
    }
    
     void ReduceBallCount()
    {
        balls--;
        ballsLeft.text = "Balls left - " + balls.ToString();

        if(balls == 0)
        {
            lastBallLost.Invoke();
        }
    }
    public void AddLastBallLostListener(UnityAction listener)
    {
        lastBallLost.AddListener(listener);
    }

}
