using System.Collections;
using UnityEngine;

public class UnbreadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject unbreadPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint_2;

    private bool switchSpawnPoint = false;
    
    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        int lvl = GameManager.Instance.Level;
        float delay = .75f;

        for (int i = 0; i < lvl; i++)
        {
            switchSpawnPoint = !switchSpawnPoint;
            yield return new WaitForSeconds(delay);
            if (switchSpawnPoint)
            {
                Instantiate(unbreadPrefab, spawnPoint.position, Quaternion.identity);
            }else
            {
                Instantiate(unbreadPrefab, spawnPoint_2.position, Quaternion.identity);
            }
        }

        Debug.Log("All Unbreads spawned.");
        TimeTracker.Instance.StartTrack();
        yield return new WaitForSeconds(10f);
        GameManager.Instance.LevelUp();


    }

}
