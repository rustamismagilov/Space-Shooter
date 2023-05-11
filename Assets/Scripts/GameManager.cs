using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    private bool _isGameOver;

    private UIManager _uIManager;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _pauseMenuPanel;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            // Get the active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Reload the appropriate scene
            if (currentScene.name == "Singleplayer")
            {
                SceneManager.LoadScene("Singleplayer");
            }
            else if (currentScene.name == "COOP")
            {
                SceneManager.LoadScene("COOP");
            }
        }

        if ((SceneManager.GetSceneByName("SinglePlayer").isLoaded || SceneManager.GetSceneByName("COOP").isLoaded) && Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumePlay()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() called.");
        _isGameOver = true;
    }
}
