using System;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// This class handles all events.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        public static event Action OnGameOver;

        public static event Action OnGamePaused;

        public static event Action OnGameResumed;
    
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

        public void RaiseOnGameOver()
        {
            OnGameOver?.Invoke();
        }

        public void RaiseOnGamePaused()
        {
            OnGamePaused?.Invoke();
        }

        public void RaiseOnGameResumed()
        {
            OnGameResumed?.Invoke();
        }
    }
}