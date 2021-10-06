using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] Transform _canvas;
    string _menuScene = "Menu";


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    void Pause()
    {
        _canvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        _canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(_menuScene);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _canvas.gameObject.SetActive(false);
    }
}
