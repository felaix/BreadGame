using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float speed = 25f;

    private void Update()
    {
        Vector2 direction = Vector2.right;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SpawnFire()
    {
        Instantiate(firePrefab, transform);
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health hp = collision.GetComponent<Health>();

        if (hp != null)
        {
            if (hp.gameObject.CompareTag("Player")) return;
            speed = 0f;
            hp.Damage(10f);
        }
    }
}
