using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TMP_Text levelTMP;


    public void ActivateGameOverUI(int levelReached)
    {
        gameObject.SetActive(true);
        levelReached -= 2;
        levelTMP.text = "Reached Level: " + levelReached.ToString();
    }
}
