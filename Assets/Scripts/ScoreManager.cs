using UnityEngine;
using System;

public class ScoreManager : Singleton<ScoreManager>
{
    public int Score { get; private set; } = 0;
    public int Turns { get; private set; } = 0;

    public Action OnScoreChanged;
    public Action OnTurnsChanged;
    public event Action OnGameOver;

    private GameManager gameManager;
    private LevelManager levelManager;

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score: {Score}");
        OnScoreChanged?.Invoke();
        CheckGameOver();
    }

    public void ResetScore()
    {
        Score = 0;
        Debug.Log("Score reset.");
        OnScoreChanged?.Invoke();
    }

    public void AddTurns(int amount)
    {
        Turns += amount;
        Debug.Log($"Turns: {Turns}");
        OnTurnsChanged?.Invoke();
    }

    public void ResetTurns()
    {
        Turns = 0;
        Debug.Log("Turns reset.");
        OnTurnsChanged?.Invoke();
    }

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }
        
        gameManager.OnMatched += HandleMatch;
    }
    private void HandleMatch()
    {
        AddScore(1); 
    }

    private void CheckGameOver()
    {
        if (Score == levelManager.Level)
        {
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
        }
    }
}