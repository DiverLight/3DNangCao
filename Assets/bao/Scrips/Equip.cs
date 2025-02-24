// Equip.cs (Add EquipPrefab)
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquip", menuName = "Inventory/Equip")]
public class Equip : ScriptableObject
{
    public int Id;
    public string EquipName;
    public Sprite EquipIcon;
    public string Describe;
    public GameObject EquipPrefab; // Add this line

    [System.Serializable]
    public class MaterialRequirement
    {
        public Item material;
        public int amount;
    }

    public List<MaterialRequirement> materialsNeeded;
}