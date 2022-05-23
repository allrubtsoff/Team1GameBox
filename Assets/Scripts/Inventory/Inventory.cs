using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;
    public Sprite CommonSlotImage { get { return commonInventorySlotIcon; } }

    private List<Item> inventory = new List<Item>();
    private readonly int maxInventorySlots = 2;

    public bool AddItem(Item item)
    {
        if(inventory.Count <= maxInventorySlots)
        {
            HideObject(item);
            inventory.Add(item);
        }
        return inventory.Count <= maxInventorySlots;
    }

    private void HideObject(Item item)
    {
        item.gameObject.SetActive(false);  
    }

    public void UseItem(int slotId)
    {
        if (inventory.Count >= slotId+1 && inventory[slotId] != null)
        {
            Item usedItem = inventory[slotId];
            usedItem.Use();
            inventory.Remove(inventory[slotId]);
        }
    }

    public List<Item> GetItems()
    {
        return inventory;
    }
}
