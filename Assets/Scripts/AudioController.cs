using UnityEngine;
using System.Collections.Generic;



public class AusioCOntroller : MonoBehaviour
{

    private GameManager gameManager;
    private LevelManager levelManager;
    private ScoreManager scoreManager;
    private AudioManager audioManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }
        if (scoreManager == null)
        {
            scoreManager = ScoreManager.Instance;
        }
        if (audioManager == null)
        {
            audioManager = AudioManager.Instance;
        }

        gameManager.OnMatched += HandleMatch;
        gameManager.OnUnmatched += HandleUnmatch;
        scoreManager.OnGameOver += HandleLevelCompleted;

        AudioManager.Instance.PlayMusic("ThemeSong");

    }

    private void HandleMatch()
    {
        audioManager.PlaySFX("MatchSound");
        Debug.Log("Match sound played.");
    }

    private void HandleUnmatch()
    {
        audioManager.PlaySFX("UnmatchSound");
        Debug.Log("Unmatch sound played.");
    }

    private void HandleLevelCompleted()
    {
        audioManager.PlaySFX("LevelCompleteSound");
        Debug.Log("Level complete sound played.");
    }

    
}