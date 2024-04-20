using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance { get; set; }
    
    public GameObject Player { get; private set; }
    
    public int CurrentScore { get; private set; }

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
        Debug.Log("Game over!");
    }
}
