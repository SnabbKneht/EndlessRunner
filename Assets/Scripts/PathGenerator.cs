using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathGenerator : MonoBehaviour
{
    // config parameters
    
    public int NumberOfPreviousTiles => numberOfPreviousTiles;
    [SerializeField] private int numberOfPreviousTiles = 1;

    public int NumberOfNextTiles => numberOfNextTiles;
    [SerializeField] private int numberOfNextTiles = 1;
    
    // cached references

    private Transform Player { get; set; }
    
    private GameObject[] TileVariants => tileVariants;
    [SerializeField] private GameObject[] tileVariants;

    private GameObject StartTile => startTile;
    [SerializeField] private GameObject startTile;
    
    // properties
    
    private GameObject CurrentTile { get; set; }

    private int CurrentTileNumber { get; set; }

    private float NextTileStartZCoord => Config.Instance.TileLength * (CurrentTileNumber + 0.5f);

    private List<GameObject> PreviousTiles { get; set; }
    private List<GameObject> NextTiles { get; set; }
    
    // ==========

    private void Awake()
    {
        PreviousTiles = new List<GameObject>();
        NextTiles = new List<GameObject>();
    }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        
        CurrentTile = Instantiate(StartTile, Vector3.zero, Quaternion.identity);
        CurrentTileNumber = 0;
        
        InitializeTileLists();
    }

    private void Update()
    {
        UpdateCurrentTileNumberAndShiftTilesIfNeeded();
    }

    private GameObject GetRandomTile()
    {
        return TileVariants[Random.Range(0, TileVariants.Length)];
    }

    private void InitializeTileLists()
    {
        for(int i = 0; i < NumberOfPreviousTiles; i++)
        {
            var newTile = Instantiate(GetRandomTile(),
                Vector3.back * Config.Instance.TileLength * (i + 1),
                Quaternion.identity);
            PreviousTiles.Add(newTile);
        }

        for(int i = 0; i < NumberOfNextTiles; i++)
        {
            var newTile = Instantiate(GetRandomTile(),
                Vector3.forward * Config.Instance.TileLength * (i + 1),
                Quaternion.identity);
            NextTiles.Add(newTile);
        }
    }

    private void UpdateCurrentTileNumberAndShiftTilesIfNeeded()
    {
        if(Player.position.z > NextTileStartZCoord)
        {
            CurrentTileNumber++;
            ShiftTiles();
        }
    }

    private void ShiftTiles()
    {
        var pastTile = CurrentTile;
        CurrentTile = NextTiles[0];
        
        Destroy(PreviousTiles[^1]);
        
        for(int i = PreviousTiles.Count - 1; i > 0; i--)
        {
            PreviousTiles[i] = PreviousTiles[i - 1];
        }
        
        PreviousTiles[0] = pastTile;

        for(int i = 0; i < NextTiles.Count - 1; i++)
        {
            NextTiles[i] = NextTiles[i + 1];
        }

        NextTiles[^1] = Instantiate(GetRandomTile(),
            Vector3.forward * (Config.Instance.TileLength * (CurrentTileNumber + numberOfNextTiles)),
            Quaternion.identity);
    }
}
