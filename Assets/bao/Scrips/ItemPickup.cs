using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameObject ButtonE;

    private bool canPickup = false;

    private void Start()
    {
        if (ButtonE != null)
        {
            ButtonE.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            if (ButtonE != null)
            {
                ButtonE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            if (ButtonE != null)
            {
                ButtonE.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        InventoryManager.Instance.Add(Item);
        if (ButtonE != null)
        {
            ButtonE.SetActive(false); // Ẩn nút "E" khi nhặt
        }
        Destroy(gameObject);
    }
}