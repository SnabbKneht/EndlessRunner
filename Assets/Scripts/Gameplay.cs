using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    // singleton
    
    public static Gameplay Instance { get; set; }
    
    // cached references
    
    public GameObject Player { get; private set; }
    
    private SceneLoader SceneLoaderReference { get; set; }
    
    // properties
    
    public int CurrentScore { get; private set; }
    
    public bool IsGamePaused { get; private set; }

    // ==========
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Player = GameObject.FindWithTag("Player");
        SceneLoaderReference = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        CurrentScore = (int)Player.transform.position.z;
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += SceneLoaderReference.StartGame;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= SceneLoaderReference.StartGame;
    }

    public void SwitchGamePause()
    {
        if(IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    
    private void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
}
