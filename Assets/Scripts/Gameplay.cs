using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    // singleton
    
    public static Gameplay Instance { get; set; }
    
    // config parameters

    public string GameSceneName => gameSceneName;
    [SerializeField] private string gameSceneName;
    
    // cached references
    
    public GameObject Player { get; private set; }
    
    // properties
    
    public int CurrentScore { get; private set; }

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
    }

    private void Update()
    {
        CurrentScore = (int)Player.transform.position.z;
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += FinishGame;
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= FinishGame;
    }

    private void FinishGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
