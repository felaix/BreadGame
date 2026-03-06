using UnityEngine;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Sprite redGunSprite;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject errorNotEnoughMoney;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void BuyBomb()
    {
        Player.Instance.AddBomb();
    }

    public void BuyWeed()
    {
        Player.Instance.SmokeWeed();
    }

    public void BuyRedGun()
    {
        Player.Instance.AssignNewGun(redGunSprite);
        Player.Instance.AssignNewBullet(bulletPrefab);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }


}
