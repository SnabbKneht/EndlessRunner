using System.Collections;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class FootstepsSoundPlayer : MonoBehaviour
    {
        // config parameters
    
        private AudioClip[] Clips => clips;
        [SerializeField] private AudioClip[] clips;

        private float SecondsBetweenFootsteps => secondsBetweenFootsteps;
        [SerializeField] private float secondsBetweenFootsteps;

        private float Volume => volume;
        [SerializeField] private float volume;
    
        // cached references

        private AudioSource AudioSourceReference { get; set; }
    
        private PlayerMovement Player { get; set; }

        // ==========

        private void Start()
        {
            AudioSourceReference = GetComponent<AudioSource>();
            Player = Gameplay.Gameplay.Instance.Player.GetComponent<PlayerMovement>();
            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            while(true)
            {
                if(Player.IsGrounded)
                {
                    AudioSourceReference.PlayOneShot(GetRandomClip(), Volume);
                    yield return new WaitForSeconds(SecondsBetweenFootsteps);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    
        private AudioClip GetRandomClip()
        {
            return Clips[Random.Range(0, Clips.Length)];
        }
    }
}
