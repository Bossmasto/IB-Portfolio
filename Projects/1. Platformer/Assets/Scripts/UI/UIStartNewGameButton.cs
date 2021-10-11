using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartNewGameButton : MonoBehaviour
{
    [SerializeField] string _levelName;

    public string LevelName => _levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
