using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    public static GameStatsUI Instance;

    public TMP_Text coinTMP;
    public TMP_Text distanceTMP;
    public TMP_Text spdTMP;
    public TMP_Text powerTMP;
    public TMP_Text cdTMP;
    public TMP_Text killsTMP;
    public TMP_Text rangeTMP;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetGameUI(StatType.Coin, GameManager.Instance.Coins.ToString());
    }

    public void SetGameUI(StatType type, string text)
    {
        switch (type)
        {
            case StatType.None:
                break;
            case StatType.Coin:
                coinTMP.text = text;
                break;
            case StatType.Distance:
                distanceTMP.text = text;
                break;
            case StatType.Speed:
                spdTMP.text = text;
                break;
            case StatType.Power:
                powerTMP.text = text;
                break;
            case StatType.Range:
                rangeTMP.text = text;
                break;
            case StatType.Cooldown:
                cdTMP.text = text;
                break;
            case StatType.Kills:
                killsTMP.text = text;
                break;
            default:
                break;
        }
    }

}

public enum StatType
{
    None,
    Coin,
    Distance,
    Speed,
    Power,
    Range,
    Cooldown,
    Kills
}
