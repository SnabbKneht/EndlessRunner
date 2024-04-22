using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // singleton
    
    public static MusicPlayer Instance { get; private set; }
    
    // config parameters

    private AudioClip IntroClip => introClip;
    [SerializeField] private AudioClip introClip;

    private AudioClip LoopClip => loopClip;
    [SerializeField] private AudioClip loopClip;
    
    // cached references
    
    private AudioSource IntroSource { get; set; }
    
    private AudioSource LoopSource { get; set; }
    
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

        IntroSource = gameObject.AddComponent<AudioSource>();
        LoopSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        IntroSource.clip = IntroClip;
        IntroSource.loop = false;
        IntroSource.Play();
        
        LoopSource.clip = LoopClip;
        LoopSource.loop = true;
        LoopSource.PlayDelayed(IntroClip.length);
    }
}
