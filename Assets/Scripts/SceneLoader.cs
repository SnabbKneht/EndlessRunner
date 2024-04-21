using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // singleton
    
    public static SceneLoader Instance { get; set; }
    
    // config parameters

    public string GameSceneName => gameSceneName;
    [SerializeField] private string gameSceneName;
    
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
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
