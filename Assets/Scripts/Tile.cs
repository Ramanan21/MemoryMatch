using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject backFace;
    [SerializeField]
    public GameObject frontFace;
    [SerializeField]
    private Button tileButton;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ScoreManager scoreManager;
    
    public int TileNumber { get; set; }

    [SerializeField]
    private bool isFlipped = false;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if (scoreManager == null)
        {
            scoreManager = ScoreManager.Instance;
        }

    }

    void Start()
    {
        tileButton?.onClick.AddListener(OpenTile);
        CloseTile();
    }
     private void OnEnable()
    {
        if (gameManager != null)
        {
            gameManager.OnMatched += HandleMatch;
            gameManager.OnUnmatched += HandleUnmatch;
        }
    }

    private void OnDisable()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        if (gameManager != null)
        {
            gameManager.OnMatched -= HandleMatch;
            gameManager.OnUnmatched -= HandleUnmatch;
        }
    }

    private void OpenTile()
    {
        if (isFlipped)
        {
            return;
        }
        isFlipped = true;
        backFace?.SetActive(false);
        frontFace?.SetActive(true);
        gameManager.ChangeTileOrder();
        if (gameManager.tileOrder == TileOrder.First)
        {
            Debug.Log("First tile selected: " + TileNumber);
            gameManager.FirstTileNumber = TileNumber;
        }
        else if (gameManager.tileOrder == TileOrder.Second)
        {
            Debug.Log("Second tile selected: " + TileNumber);
            gameManager.SecondTileNumber = TileNumber;
            gameManager.CheckMatch();
            scoreManager.AddTurns(1);
        }
    }

    private void CloseTile(float delay = 0.5f)
    {
        StartCoroutine(CloseTileCoroutine(delay));
    }
    private IEnumerator CloseTileCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        backFace?.SetActive(true);
        isFlipped = false;
    }
    private void CloseTile()
    {
        backFace?.SetActive(true);
        isFlipped = false;
    }

    private void HideTile(float delay = 0.5f)
    {
        StartCoroutine(HideTileCoroutine(delay));
    }
    private IEnumerator HideTileCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        frontFace?.SetActive(false);
        backFace?.SetActive(false);
    }
    private void HideTile()
    {
        frontFace?.SetActive(false);
        backFace?.SetActive(false);
    }

    private void HandleMatch()
    {
        if (!isFlipped)
        {
            return;
        }
        Debug.Log($"{gameObject.name} received MATCH event!");
        gameManager.tileOrder = TileOrder.None;
        HideTile(1f);
        RemoveEvents();
    }

    private void HandleUnmatch()
    {
        if (!isFlipped)
        {
            return;
        }
        Debug.Log($"{gameObject.name} received UNMATCH event!");
        gameManager.tileOrder = TileOrder.None;
        CloseTile(1f); 
    }
}
