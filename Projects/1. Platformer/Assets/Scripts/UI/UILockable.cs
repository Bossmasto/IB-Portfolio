using UnityEngine;

public class UILockable : MonoBehaviour
{
    void OnEnable()
    {
        var startButton = GetComponent<UIStartNewGameButton>();
        string key = startButton.LevelName + "Unlocked";


        int unlocked = PlayerPrefs.GetInt(key);

        if (unlocked == 0)
            gameObject.SetActive(false);
    }
}
