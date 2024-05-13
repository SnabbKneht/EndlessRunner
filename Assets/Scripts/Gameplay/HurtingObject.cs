using UnityEngine;

namespace Gameplay
{
    public class HurtingObject : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            EventManager.Instance.RaiseOnGameOver();
        }
    }
}
