using UnityEngine;

public class GunUpgrade : MonoBehaviour
{

    public GameObject bulletUpgradePrefab;

    public void UpgradeCooldown()
    {
        Player.Instance.fireCooldown -= .1f;
        GameStatsUI.Instance.SetGameUI(StatType.Cooldown, Player.Instance.fireCooldown.ToString());
    }

    public void UpgradeGun()
    {
        FindAnyObjectByType<Player>().AssignNewBullet(bulletUpgradePrefab);
    }

    public void ChangeGunSprite(Sprite spr)
    {
        Player.Instance.AssignNewGun(spr);
    }
}
