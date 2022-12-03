using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameProg2.Items;

namespace GameProg2.Inventory.Model
{
    public class GameInventory
    {
        private List<InventoryItem> inventoryItems;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryContentUpdated;

        public int InventorySize { get; private set; }
        private bool IsInventoryFull
            => inventoryItems.Where(item => item.IsEmpty).Any();


        // Constructors
        public GameInventory()
        {
            InventorySize = 12;
            InitializeInventory(InventorySize);
        }
        /// <summary>
        /// Creates New Game Inventory of given Inventory Size.
        /// </summary>
        /// <param name="InventorySize">Amount of Inventory slots in New Game Inventory</param>
        public GameInventory(int InventorySize)
        {
            this.InventorySize = InventorySize;
            InitializeInventory(InventorySize);
        }


        // Methods
        private void InitializeInventory(int InventorySize)
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < InventorySize; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }


        /// <returns>Dictionary of Non-Empty Inventory Slots using List Index as Key.</returns>
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        /// <summary>
        /// Tries to insert given item of given quantity to the inventory.
        /// </summary>
        /// <param name="quantity">Quantity of items being added</param>
        /// <param name="item">The Item Data of the item being added</param>
        /// <returns>Quantity of Items NOT Added to the Inventory</returns>
        public int AddItem(int quantity, ItemData item)
        {
            if (item.StackSize == 1)
            {
                while (quantity > 0 && !IsInventoryFull)
                {
                    quantity -= AddItemToFirstEmptySlot(1, item);
                }
            }
            else
            {
                quantity = AddStackableItem(quantity, item);
            }
            InformInventoryContentChange();
            return quantity;
        }
        /// <summary>
        /// Tries to insert item, of quantity and type specified by InventoryItem, to the inventory.
        /// </summary>
        /// <param name="item">Inventory item with specified quantity and type.</param>
        /// <returns>Quantity of Items NOT Added to the Inventory</returns>
        public int AddItem(InventoryItem item)
        {
            return AddItem(item.quantity, item.ItemData);
        }

        /// <summary>
        /// Adds an item of specified quantity in the first empty inventory slot.
        /// </summary>
        /// <param name="quantity">Quantity of items being added</param>
        /// <param name="item">The Item Data of the item being added</param>
        /// <returns>The amount of items successfully added</returns>
        private int AddItemToFirstEmptySlot(int quantity, ItemData item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem(quantity, item);
                    return quantity;
                }
            }
            return 0;
        }

        /// <summary>
        /// Adds Stackable items to any existing stacks, then adds the remaining
        /// to empty inventory slots
        /// </summary>
        /// <param name="quantity">Quantity of items being added</param>
        /// <param name="item">The Item Data of the item being added</param>
        /// <returns>Quantity of Items NOT Added to the Inventory</returns>
        private int AddStackableItem(int quantity, ItemData item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].IsEmpty
                    && inventoryItems[i].ItemData.ItemID == item.ItemID)
                {
                    int stackSpaceLeftInItemSlot = inventoryItems[i].ItemData.StackSize - inventoryItems[i].quantity;
                    if (quantity > stackSpaceLeftInItemSlot)
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].ItemData.StackSize);
                        quantity -= stackSpaceLeftInItemSlot;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        return 0;
                    }
                }
            }
            while (quantity > 0 && !IsInventoryFull)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.StackSize);
                quantity -= newQuantity;
                AddItemToFirstEmptySlot(newQuantity, item);
            }
            return quantity;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        /// <summary>
        /// Swaps contents of inventory slots at given Item Indexes with one another.
        /// </summary>
        /// <param name="itemIndex_1">List index of inventory slot to be swapped</param>
        /// <param name="itemIndex_2">List index of inventory slot to be swapped</param>
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            (inventoryItems[itemIndex_1], inventoryItems[itemIndex_2]) =
                (inventoryItems[itemIndex_2], inventoryItems[itemIndex_1]);
            InformInventoryContentChange();
        }

        /// <summary>
        /// Invokes the "OnInventoryContentUpdated" Event, passing the current inventory
        /// state.
        /// </summary>
        private void InformInventoryContentChange()
        {
            OnInventoryContentUpdated?.Invoke(GetCurrentInventoryState());
        }
    }



    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemData ItemData;
        public bool IsEmpty => ItemData == null;

        public InventoryItem(int Quantity, ItemData ItemData)
        {
            this.quantity = Quantity;
            this.ItemData = ItemData;
        }

        public InventoryItem ChangeQuantity(int NewQuantity)
            => new()
            {
                ItemData = this.ItemData,
                quantity = NewQuantity
            };
        public static InventoryItem GetEmptyItem()
            => new()
            {
                ItemData = null,
                quantity = 0
            };
    }

}
