using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject titleMenu;

    public void LoadSingleplayerGame()
    {
        SceneManager.LoadScene("Singleplayer");
    }

    public void LoadMultiplayerGame()
    {
        SceneManager.LoadScene("COOP");
    }

    public void HideTitleMenu()
    {
        settingsMenu.SetActive(true);
        titleMenu.SetActive(false);
    }

    public void HidePauseMenu()
    {
        settingsMenu.SetActive(false);
        titleMenu.SetActive(true);
    }
}
