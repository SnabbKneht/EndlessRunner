using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// This class handles the game configuration.
    /// </summary>
    public class Config : MonoBehaviour
    {
        public static Config Instance { get; private set; }
    
        public float LaneWidth => laneWidth;
        [SerializeField] private float laneWidth;

        public float TileLength => tileLength;
        [SerializeField] private float tileLength;

        public Vector3 InitialPlayerPosition => initialPlayerPosition;
        [SerializeField] private Vector3 initialPlayerPosition;

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
    }
}
