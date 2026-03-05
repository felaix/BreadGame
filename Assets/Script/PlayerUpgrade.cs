using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public void UpgradeSpeed(float amount)
    {
        Player.Instance.AddSpeed(amount);
        GameStatsUI.Instance.SetGameUI(StatType.Speed, Player.Instance.GetMoveSpeed().ToString());
    }
}
