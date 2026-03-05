using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 6f;   // sideways push
    [SerializeField] private float knockbackUp = 2f;      // small lift

    public float knockLockTime = 0.12f;
    public float KnockLockUntil { get; private set; }

    private Animator anim;
    private bool isPlayer = false;
    private Rigidbody2D rb;

    public bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        hpSlider.gameObject.SetActive(false);

        if (gameObject.GetComponent<Player>() != null) { isPlayer = true; }
    }


    public void Damage(float dmg)
    {
        if (hpSlider == null) return;
        if (isDead) return;

        currentHP -= dmg;
        hpSlider.value = currentHP;

        hpSlider.gameObject.SetActive(true);

        var undead = GetComponent<Undead>();
        if (undead != null)
        {
            undead.KnockbackRight();
        }

        // Knockback (always to the right)
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * knockbackUp, ForceMode2D.Impulse);
        }

        if (currentHP < 0)
        {
            hpSlider.gameObject.SetActive(false);
            if (!isDead && anim != null) anim.Play("Dead");
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            if (isPlayer) { GameManager.Instance.GameOver(); }
            isDead = true;
            StartCoroutine(DelayDeactivation());
            return;
        }

        if (bloodPrefab == null) return;
        GameObject bloodInstance = Instantiate(bloodPrefab, transform);
        Destroy(bloodInstance, .2f);

    }

    private IEnumerator DelayDeactivation() {
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
    }



}
