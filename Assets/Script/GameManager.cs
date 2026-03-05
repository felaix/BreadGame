using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnbreadSpawner spawner;

    public GameObject levelUpCanvas;
    public GameOverUI gameOverCanvas;
    private GameStatsUI statsUI;

    public int Level = 0;
    public int Coins = 0;
    public int Distance = 0;
    public int Kills = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        Distance = (int) Player.Instance.GetDistance();
        statsUI.SetGameUI(StatType.Distance, Distance.ToString());
    }

    private void Start()
    {
        LevelUp();
        statsUI = GameStatsUI.Instance;
    }

    public void AddCoin()
    {
        Coins++;
        statsUI.SetGameUI(StatType.Coin, Coins.ToString());
    }

    public void AddKill()
    {
        Kills++;
        statsUI.SetGameUI(StatType.Kills, Kills.ToString());
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
