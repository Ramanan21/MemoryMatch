using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager :  Singleton<GameManager>
{
    public TileOrder tileOrder = TileOrder.None;

    public event Action OnMatched;
    public event Action OnUnmatched;

    public int FirstTileNumber { get; set; }
    public int SecondTileNumber { get; set; }

    void Awake()
    {
        Debug.Log("GameManager Awake");
    }

    public void ChangeTileOrder()
    {
        Debug.Log("Tile selected");
        if(tileOrder == TileOrder.None)
        {
            tileOrder = TileOrder.First;
        }
        else if(tileOrder == TileOrder.First)
        {
            tileOrder = TileOrder.Second;
        }
        else
        {
            tileOrder = TileOrder.None;
        }
    }

    public void Match()
    {
        Debug.Log("Tiles matched!");
        OnMatched?.Invoke();
    }

    public void Unmatch()
    {
        Debug.Log("Tiles did not match.");
        OnUnmatched?.Invoke();
    }

    public void CheckMatch()
    {
        if (FirstTileNumber == SecondTileNumber)
        {
            Match();
        }
        else
        {
            Unmatch();
        }
    }
}
