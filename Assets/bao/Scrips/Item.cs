using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int Id;
    public string itemName;
    public Sprite icon;
    public int Stack;
}   