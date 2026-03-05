using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float damage = 2f;
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private GameObject explosionPrefab;

    private Animator anim;
    private Vector2 direction = Vector2.right;
    private bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Optional: flip sprite based on direction
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        if (canMove)
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Undead")) return;

        CameraShake.Instance.Shake();

        Health undeadHP = collision.GetComponent<Health>();
        undeadHP.Damage(damage);

        canMove = false;
        direction = Vector2.zero;

        if (anim != null)
            anim.Play("BulletFX");

        if (explosionPrefab != null)
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform);
            Destroy(explosionInstance, .24f);
        }

        Destroy(gameObject, 0.3f);
    }
}