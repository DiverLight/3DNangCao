using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    public TextMeshProUGUI equipNameText;
    public TextMeshProUGUI equipdescribeText;
    public TextMeshProUGUI materialsText;
    public Button craftButton; // Tham chiếu đến nút Crafting

    private Equip currentEquipToCraft;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCraftingInfo(Equip equip)
    {
        currentEquipToCraft = equip;
        equipNameText.text = equip.EquipName;
        equipdescribeText.text = equip.Describe;


        string materialsString = "Nguyên liệu: \n";
        bool canCraft = true; // Biến kiểm tra xem có đủ vật liệu không

        foreach (Equip.MaterialRequirement requirement in equip.materialsNeeded)
        {
            materialsString += "- " + requirement.amount + " " + requirement.material.itemName + "\n";
            if (InventoryManager.Instance.GetItemAmount(requirement.material.Id) < requirement.amount)
            {
                canCraft = false; // Không đủ vật liệu
            }
        }
        materialsText.text = materialsString;
        craftButton.gameObject.SetActive(canCraft); // Hiển thị nút Crafting nếu đủ vật liệu
    }

    public void CraftItem()
    {
        if (currentEquipToCraft != null)
        {
            // Kiểm tra lại vật liệu trước khi craft (để đảm bảo không có thay đổi trong quá trình)
            bool canCraft = true;
            foreach (Equip.MaterialRequirement requirement in currentEquipToCraft.materialsNeeded)
            {
                if (InventoryManager.Instance.GetItemAmount(requirement.material.Id) < requirement.amount)
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                // Xóa vật liệu khỏi inventory
                foreach (Equip.MaterialRequirement requirement in currentEquipToCraft.materialsNeeded)
                {
                    InventoryManager.Instance.RemoveItemById(requirement.material.Id, requirement.amount);
                }
                // Thêm trang bị vào inventory
                InventoryManager.Instance.AddEquip(currentEquipToCraft); // Thêm Equip
                craftButton.gameObject.SetActive(false);
            }
        }
    }
}