using UnityEngine;


public enum ItemTypes
{
    Miscellaneous = 0,
    Equipment = 1,
    Consumable = 2
}
[CreateAssetMenu(menuName = "Item Data", fileName = "New Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] Sprite designatedSprite;
    [SerializeField] string itemID;
    [field: TextArea]
    [SerializeField] string description = "Item Description";
    [SerializeField] ItemTypes itemType;
    [SerializeField] int stackSize = 1;
    

    public Sprite DesignatedSprite { get => designatedSprite; }
    public string ItemID { get => itemID; }
    public string Description { get => description; }
    public ItemTypes ItemType { get => itemType; }
    public int StackSize { get => stackSize; }
}
