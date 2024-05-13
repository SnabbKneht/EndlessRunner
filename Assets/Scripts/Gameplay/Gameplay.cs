using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        // singleton
        /// <summary>
        /// This class handles gameplay-related things.
        /// </summary>
        public static Gameplay Instance { get; set; }
    
        // cached references
    
        public GameObject Player { get; private set; }
    
        private SceneLoader SceneLoaderReference { get; set; }

        private PauseMenuUI PauseMenu => pauseMenu;
        [SerializeField] private PauseMenuUI pauseMenu;
    
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
            EventManager.OnGamePaused += PauseMenu.Show;
            EventManager.OnGameResumed += PauseMenu.Hide;
        }

        private void OnDisable()
        {
            EventManager.OnGameOver -= SceneLoaderReference.StartGame;
            EventManager.OnGamePaused -= PauseMenu.Show;
            EventManager.OnGameResumed -= PauseMenu.Hide;
        }

        public void OnSwitchGamePause(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
        
            if(IsGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    
        public void PauseGame()
        {
            Time.timeScale = 0f;
            IsGamePaused = true;
            EventManager.Instance.RaiseOnGamePaused();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            IsGamePaused = false;
            EventManager.Instance.RaiseOnGameResumed();
        }
    }
}
