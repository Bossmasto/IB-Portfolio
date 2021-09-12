using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour
{
    /// <summary>
    /// Handles the on click event from the back button
    /// </summary>
    public void HandleBackButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
    }
}
