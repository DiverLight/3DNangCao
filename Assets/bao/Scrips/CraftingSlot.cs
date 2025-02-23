using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot : MonoBehaviour
{
    public Equip EquipToCraft;
    public Image image;
    public TextMeshProUGUI describe;
    public TextMeshProUGUI equipName;
    public CraftingManager craftingManager; 

    private void Start()
    {


        if (EquipToCraft != null)
        {
            image.sprite = EquipToCraft.EquipIcon;
            equipName.text = EquipToCraft.EquipName;
            describe.text = EquipToCraft.Describe;
        }
    }

    public void OnSlotClicked()
    {
        if (EquipToCraft != null && craftingManager != null)
        {
            craftingManager.ShowCraftingInfo(EquipToCraft);
        }
    }
}