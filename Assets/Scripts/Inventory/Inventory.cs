using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;
    public Sprite CommonSlotImage { get { return commonInventorySlotIcon; } }

    private List<Item> inventory = new List<Item>();
    private readonly int maxInventorySlots = 2;

    public static event Action<int,Item,bool> UpdateUI;

    public bool AddItem(Item item)
    {
        if(inventory.Count <= maxInventorySlots)
        {
            HideObject(item);
            TryUpdateUI(inventory.Count,item,false);
            inventory.Add(item);
        }
        return inventory.Count <= maxInventorySlots;
    }

    private void HideObject(Item item)
    {
        item.gameObject.SetActive(false);
    }

    private void TryUpdateUI(int slotId, Item item, bool isUsed)
    {
        if (UpdateUI != null)
            UpdateUI(slotId,item,isUsed);
    }

    public void UseItem(int slotId)
    {
        if (inventory.Count >= slotId+1 && inventory[slotId] != null)
        {
            Item usedItem = inventory[slotId];
            usedItem.Use();
            TryUpdateUI(slotId, usedItem, true);
            inventory.Remove(usedItem);
        }
    }

    public List<Item> GetItems()
    {
        return inventory;
    }
}
