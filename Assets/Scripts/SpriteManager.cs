using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    public List<Sprite> spriteList = new List<Sprite>();

    void Awake()
    {
        LoadAllSpritesIntoList();
    }

    void LoadAllSpritesIntoList()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("2D");

        if (loadedSprites.Length == 0)
        {
            Debug.LogWarning("No sprites found in Resources/Sprites/");
        }
        else
        {
            spriteList = new List<Sprite>(loadedSprites);
            Debug.Log($"Loaded {spriteList.Count} sprites into list.");
        }
    }
}
