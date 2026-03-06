using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject highlight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlight.SetActive(false);
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted");
        shopUI.SetActive(true);
    }
}
