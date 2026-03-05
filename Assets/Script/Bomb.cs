using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private GameObject firePrefab;

    public void SpawnFire()
    {
        Instantiate(firePrefab, transform.position, Quaternion.identity);
    }
}
