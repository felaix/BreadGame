using UnityEngine;

public class GunUpgrade : MonoBehaviour
{

    public GameObject bulletUpgradePrefab;

    public void UpgradeCooldown()
    {
        Player.Instance.fireCooldown -= .1f;
    }

    public void UpgradeGun()
    {
        FindAnyObjectByType<Player>().AssignNewBullet(bulletUpgradePrefab);
    }
}
