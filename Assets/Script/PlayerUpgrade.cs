using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{


    public void UpgradeSpeed(float amount)
    {
        Player.Instance.AddSpeed(amount);
    }
}
