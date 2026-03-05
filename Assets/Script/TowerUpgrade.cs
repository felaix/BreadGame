using TMPro;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    private float towerSpeed = 0f;
    public TMP_Text upgradeText;
    public Tower assignedTower;
    private bool isBuild = false;

    private void OnEnable()
    {
        if (isBuild)
        {
            upgradeText.text = "SPD++ " + assignedTower.name;
        }
    }

    public void BuildTower()
    {
        if (!isBuild)
        {
            assignedTower.gameObject.SetActive(true);
            isBuild = true;
        }
    }

    public void ApplyTowerUpgrade()
    {
        assignedTower.gameObject.SetActive(true);

        if (!isBuild)
        {
            upgradeText.text = "SPD++ " + assignedTower.name;
            isBuild = true;
            towerSpeed += .1f;
            return;
        }

        assignedTower.delay -= towerSpeed;
    }
}
