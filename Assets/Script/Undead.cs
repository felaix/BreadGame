using System.Collections;
using UnityEngine;

public class Undead : MonoBehaviour
{
    public float speed = 5f;
    public float stopDistance = 0.1f;

    private Rigidbody2D rb;
    private Animator anim;
    private Health hp;
    private Transform target;
    private Health targetHp;

    [Header("Knockback")]
    public float knockSpeed = 8f;
    public float knockDuration = 0.12f;

    private float knockTimer;

    private bool attacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = GetComponent<Health>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tower") || collision.CompareTag("Player"))
        {
            Debug.Log("Player or Tower in range");
            StartCoroutine(AttackLoop());
        }
    }

    public void KnockbackRight()
    {
        knockTimer = knockDuration;
    }

    private void UpdateTarget()
    {
        GameObject[] turretObjects = GameObject.FindGameObjectsWithTag("Tower");

        Transform closestTurret = null;
        float closestX = float.MinValue;

        foreach (GameObject turretObj in turretObjects)
        {
            Health turretHp = turretObj.GetComponent<Health>();
            if (turretHp == null || turretHp.isDead) continue;

            // Only turrets on the left
            if (turretObj.transform.position.x < transform.position.x)
            {
                if (turretObj.transform.position.x > closestX)
                {
                    closestX = turretObj.transform.position.x;
                    closestTurret = turretObj.transform;
                }
            }
        }

        if (closestTurret != null)
        {
            target = closestTurret;
            targetHp = closestTurret.GetComponent<Health>();
            return;
        }

        // No turrets alive → target player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
            Debug.Log("targeted player");
            targetHp = playerObj.GetComponent<Health>();
        }

    }

    private void FixedUpdate()
    {
        if (hp.isDead)
        {
            anim.SetBool("isMoving", false);
            return;
        }

        // If knocked, move RIGHT and skip normal movement
        if (knockTimer > 0f)
        {
            knockTimer -= Time.fixedDeltaTime;
            rb.MovePosition(rb.position + Vector2.right * knockSpeed * Time.fixedDeltaTime);
            anim.SetBool("isMoving", false);
            return;
        }
        if (target == null) return;

        float distanceX = transform.position.x - target.position.x;

        if (distanceX <= stopDistance)
        {
            anim.SetBool("isMoving", false);
            if (!attacking) StartCoroutine(AttackLoop());
            return;
        }

        rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
        anim.SetBool("isMoving", true);
    }

    private IEnumerator AttackLoop() 
    {
        Debug.Log("Attack Loop");
        while (!targetHp.isDead && targetHp != null)
        {
            Debug.Log("attacking");
            UpdateTarget();
            attacking = true;
            anim.Play("Attack");
            targetHp.Damage(1f);
            Debug.Log("Attack");
            yield return new WaitForSeconds(.5f);
        }

        attacking = false;
        
        yield return null;
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        UpdateTarget();
    }
}