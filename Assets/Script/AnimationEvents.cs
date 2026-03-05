using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Void")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPosition;
    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPosition.position, Quaternion.identity);
    }

    public void DestroyVoid()
    {
        Destroy(gameObject);
    }
}
