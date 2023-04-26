using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSingleplayerGame()
    {
        SceneManager.LoadScene("Singleplayer");
    }

    public void LoadMultiplayerGame()
    {
        SceneManager.LoadScene("COOP");
    }
}
