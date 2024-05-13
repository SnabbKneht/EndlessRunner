using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class SceneLoader : MonoBehaviour
    {
        // singleton
    
        public static SceneLoader Instance { get; set; }
    
        // config parameters

        public string GameSceneName => gameSceneName;
        [SerializeField] private string gameSceneName;
    
        public string MenuSceneName => menuSceneName;
        [SerializeField] private string menuSceneName;
    
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

        public void LoadMenu()
        {
            SceneManager.LoadScene(MenuSceneName);
        }
    }
}
