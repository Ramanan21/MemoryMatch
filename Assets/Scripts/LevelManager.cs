using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

 public int Level { get; private set; } = 2;
    private const int MinLevel = 2;
    private const int MaxLevel = 8;

    public void SetLevel(int newLevel)
    {
        Level = Mathf.Clamp(newLevel, MinLevel, MaxLevel);
        Debug.Log("Level set to: " + Level);
    }

    public void IncreaseLevel()
    {
        if (Level < MaxLevel)
        {
            Level++;
            Debug.Log("Level increased to: " + Level);
        }
        else
        {
            Debug.Log("Max level reached: " + MaxLevel);
        }
    }

    public void ResetLevel()
    {
        Level = MinLevel;
        Debug.Log("Level reset to: " + Level);
    }
    
}