using UnityEngine;

[CreateAssetMenu(menuName = "Item Data", fileName = "New Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] Sprite designatedSprite;
    [SerializeField] string itemID;
    [field: TextArea]
    [SerializeField] string description = "Item Description";
    [SerializeField] int itemType;
    [SerializeField] int stackSize = 1;
    

    public Sprite DesignatedSprite { get => designatedSprite; }
    public string ItemID { get => itemID; }
    public string Description { get => description; }
    public int ItemType { get => itemType; }
    public int StackSize { get => stackSize; }
}
