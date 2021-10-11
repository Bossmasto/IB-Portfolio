using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    [SerializeField] Text gameoverText;
    void Start()
    {
        //pause the game
        Time.timeScale = 0;
    }
    public void SetScore(int score)
    {
        gameoverText.text = "Game Over!\nScore: " + score;
    }

    public void HandleQuitButtonClick()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
