using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnbreadSpawner spawner;

    public GameObject levelUpCanvas;
    public GameOverUI gameOverCanvas;
    public TMP_Text coinTMP;

    public int Level = 0;
    public int Coins = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelUp();
    }

    public void AddCoin()
    {
        Coins++;
        coinTMP.text = Coins.ToString();
    }

    public void GameOver()
    {
        gameOverCanvas.ActivateGameOverUI(Level);
        Time.timeScale = 0f;
    }

    public void LevelUp()
    {
        Level++;
        spawner.Spawn();
        levelUpCanvas.SetActive(true);
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
