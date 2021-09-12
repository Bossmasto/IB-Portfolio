using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //pause the game
        Time.timeScale = 0;
    }

    public void HandleResumeButtonClick()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void HandleQuitButtonClick()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

}
