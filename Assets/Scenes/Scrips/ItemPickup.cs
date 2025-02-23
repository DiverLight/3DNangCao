using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public Inventory inventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Kiểm tra va chạm với Player
        {
            PickupItem();
        }
    }


    void PickupItem()
    {
        inventory.AddItem(item);
        Destroy(gameObject); // Hủy vật phẩm sau khi nhặt
    }
}