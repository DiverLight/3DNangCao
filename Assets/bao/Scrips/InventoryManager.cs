using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public List<Equip> Equips = new List<Equip>();

    public Transform ItemContent;
    public GameObject InvetoryItem;
    public GameObject inventoryUI;
    private bool inventoryOpen = false;

    public Transform equipPoint; // Điểm gắn trang bị cho người chơi
    public GameObject currentEquippedObject; // Đối tượng trang bị hiện tại

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(inventoryOpen);
        }
        if (inventoryOpen)
        {
            ListItems();
        }
    }

    public void Add(Item item)
    {
        bool itemAlreadyInInventory = false;
        foreach (var existingItem in Items)
        {
            if (existingItem.Id == item.Id)
            {
                existingItem.Stack += 1;
                itemAlreadyInInventory = true;
                break;
            }
        }
        if (!itemAlreadyInInventory)
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            newItem.Id = item.Id;
            newItem.itemName = item.itemName;
            newItem.icon = item.icon;
            newItem.Stack = 1;
            Items.Add(newItem);
        }
        ListItems();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        ListItems();
    }

    public void AddEquip(Equip equip)
    {
        Equips.Add(equip);
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InvetoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemStack = obj.transform.Find("ItemStack").GetComponent<TextMeshProUGUI>();
            Button equipButton = obj.GetComponent<Button>(); //get the button component.

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemStack.text = item.Stack.ToString();
            equipButton.onClick.AddListener(() => UseItem(item)); // Add listener for using items.
        }

        foreach (var equip in Equips)
        {
            GameObject obj = Instantiate(InvetoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemStack = obj.transform.Find("ItemStack").GetComponent<TextMeshProUGUI>();
            Button equipButton = obj.GetComponent<Button>(); //get the button component.

            itemName.text = equip.EquipName;
            itemIcon.sprite = equip.EquipIcon;
            itemStack.text = "1";
            equipButton.onClick.AddListener(() => EquipItem(equip)); // Add listener for equipping equips.
        }
    }

    public int GetItemAmount(int itemId)
    {
        int amount = 0;
        foreach (Item item in Items)
        {
            if (item.Id == itemId)
            {
                amount += item.Stack;
            }
        }
        return amount;
    }

    public void RemoveItemById(int itemId, int amount)
    {
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (Items[i].Id == itemId)
            {
                if (Items[i].Stack > amount)
                {
                    Items[i].Stack -= amount;
                    break;
                }
                else if (Items[i].Stack == amount)
                {
                    Items.RemoveAt(i);
                    break;
                }
                else
                {
                    amount -= Items[i].Stack;
                    Items.RemoveAt(i);
                }
            }
        }
        ListItems();
    }

    public void EquipItem(Equip equip)
    {
        // Destroy the currently equipped object if it exists
        if (currentEquippedObject != null)
        {
            Destroy(currentEquippedObject);
        }

        // Instantiate the equip prefab at the equip point
        GameObject equipObject = Instantiate(equip.EquipPrefab, equipPoint.position, equipPoint.rotation, equipPoint);
        currentEquippedObject = equipObject;
        Debug.Log("Equipped " + equip.EquipName);
        ToggleInventory(); //close the inventory after equipping.
    }

    public void UseItem(Item item)
    {
        Debug.Log("Used " + item.itemName);
        RemoveItemById(item.Id, 1);
        // Add item use logic here
    }
}

