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

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Canvas").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene("Singleplayer");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        
    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() called.");
        _isGameOver = true;
    }
}
