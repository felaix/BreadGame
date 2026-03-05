using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Animator anim;
    public float delay;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Undead"))
        {
            // play destroy anim

            //Destroy(gameObject, .3f);
        }
    }

    private void CreateBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPos);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        CreateBullet();
        StartCoroutine(Shoot());
    }
} 
