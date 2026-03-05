using UnityEngine;

public class Coin : MonoBehaviour
{

    private bool collected = false;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;
            anim.Play("Coin_Collect");
            GameManager.Instance.AddCoin();
            Destroy(gameObject, .3f);
        }
    }
}
