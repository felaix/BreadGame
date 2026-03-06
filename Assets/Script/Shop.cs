using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject shopUI;

    public void Interact()
    {
        Debug.Log("Interacted");
        shopUI.SetActive(true);
    }
}
