using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject gamePlayMenu;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button homeButton;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    SpriteManager spriteManager;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private GameObject gameplayHolder;
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI turnsText;
    [SerializeField]
    private TextMeshProUGUI bonusText;


    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if (spriteManager == null)
        {
            spriteManager = SpriteManager.Instance;
        }
        if (scoreManager == null)
        {
            scoreManager = ScoreManager.Instance;
        }
        if (levelManager == null)
        {
            levelManager = LevelManager.Instance;
        }
    }
    void Start()
    {
        levelManager.ResetLevel();
        LoadMainMenu();
        playButton?.onClick.AddListener(LoadGamePlayMenu);
        homeButton?.onClick.AddListener(HomeButtonFunction);
        scoreManager.OnScoreChanged += UpdateScoreText;   
        scoreManager.OnTurnsChanged += UpdateTurnsText;
        scoreManager.OnGameOver += LoadMainMenuAfterGameOver;
    }

    private void HomeButtonFunction()
    {
        Debug.Log("Home button clicked");
        LoadMainMenu();
        GameReset();
    }

    private void CreatePairData(int pairs)
    {
        PairShuffler shuffler = new PairShuffler(pairs); 
        List<int> result = shuffler.ShuffledList;

        foreach (int num in result)
        {
            PopulateTiles(num);
        }
    }

    private void PopulateTiles(int num)
    {
        GameObject newTile = Instantiate(tile, gameplayHolder.transform);
        Tile tileScript = newTile.GetComponent<Tile>();
        tileScript.frontFace.GetComponent<Image>().sprite = spriteManager.spriteList[num];
        tileScript.TileNumber = num;
        newTile.name = "Tile_"+ num;
    }

    private void ClearTiles()
    {
        foreach (Transform child in gameplayHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GameReset()
    {
        scoreManager.ResetScore();
        scoreManager.ResetTurns();
    }

    private void UpdateScoreText()
    {
        scoreText.text = scoreManager.Score.ToString();
    }
    private void UpdateTurnsText()
    {
        turnsText.text = scoreManager.Turns.ToString();
    }

    private void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        gamePlayMenu.SetActive(false);
        ClearTiles();
    }

    private void LoadGamePlayMenu()
    {
        CreatePairData(levelManager.Level);
        GameReset();
        mainMenu.SetActive(false);
        gamePlayMenu.SetActive(true);
    }

    private void LoadMainMenuAfterGameOver()
    {
        StartCoroutine(LoadMainMenuCoroutine());
    }

    IEnumerator LoadMainMenuCoroutine()
    {
        levelManager.IncreaseLevel();
        yield return new WaitForSeconds(1.1f);
        LoadMainMenu();
    }
}
