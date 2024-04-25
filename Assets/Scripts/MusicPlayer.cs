using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MusicPlayer : MonoBehaviour
{
    // singleton
    
    public static MusicPlayer Instance { get; private set; }
    
    // config parameters

    private AssetReferenceAudioClip IntroClipAsset => introClipAsset;
    [SerializeField] private AssetReferenceAudioClip introClipAsset;

    private AssetReferenceAudioClip LoopClipAsset => loopClipAsset;
    [SerializeField] private AssetReferenceAudioClip loopClipAsset;
    
    // cached references
    
    private AudioSource IntroSource { get; set; }
    
    private AudioSource LoopSource { get; set; }
    
    // event
    private event Action OnAssetsLoaded;
    
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
        IntroSource.loop = false;
        LoopSource.loop = true;
        
        OnAssetsLoaded += Play;
        LoadAssets();
    }

    private void Play()
    {
        IntroSource.Play();
        LoopSource.PlayDelayed(IntroSource.clip.length);
    }
    
    private void LoadAssets()
    {
        IntroClipAsset.LoadAssetAsync().Completed += handle =>
        {
            IntroSource.clip = handle.Result;
            if(LoopSource.clip) OnAssetsLoaded?.Invoke();
        };
        LoopClipAsset.LoadAssetAsync().Completed += handle =>
        {
            LoopSource.clip = handle.Result;
            if(IntroSource.clip) OnAssetsLoaded?.Invoke();
        };
    }
}
