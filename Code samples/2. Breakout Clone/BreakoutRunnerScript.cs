using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutRunnerScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddLastBallLostListener(OnLastBallLost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLastBallLost()
    {
        EndGame();
    }

    void OnLastBlockBroken()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length == 1)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameObject gameOverMessage = Instantiate(Resources.Load("GameOverMenu")) as GameObject;
        GameOverMenu gameOverMessageScript = gameOverMessage.GetComponent<GameOverMenu>();
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        HUD hudScript = hud.GetComponent<HUD>();
        gameOverMessageScript.SetScore(hudScript.Score);
    }
}
