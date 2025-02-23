using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotParent;

    public List<Item> items = new List<Item>();

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        UpdateInventoryUI();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        // Xóa các slot cũ
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // Tạo slot mới và hiển thị thông tin vật phẩm
        foreach (Item item in items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.icon;
            slot.transform.Find("ItemName").GetComponent<Text>().text = item.name;
        }
    }
}