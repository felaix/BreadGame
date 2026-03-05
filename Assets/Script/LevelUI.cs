using System.Collections;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Transform modifierSpawnBox;
    [SerializeField] private GameObject gameStats;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play("FadeIn");
        gameStats.SetActive(false);
        StartCoroutine(DelayedTimeScale());
    }

    private void OnDisable()
    {
        gameStats.SetActive(true);
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    private IEnumerator DelayedTimeScale()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }
}
