using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;
    public Sprite CommonSlotImage { get { return commonInventorySlotIcon; } }

    private readonly int maxInventorySlots = 2;
    private Item[] inventory;

    public static event Action<int,Item,bool> UpdateUI;

    private void Start()
    {
        inventory = new Item[maxInventorySlots];
    }

    public bool AddItem(Item item)
    {
        var slotId = Array.IndexOf(inventory, null);
        if ( slotId != -1)
        {
            HideObject(item);
            TryUpdateUI(slotId,item,false);
            inventory[slotId] = item;
        }
        return inventory.Length <= maxInventorySlots;
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
        if (inventory[slotId]  != null)
        {
            Item usedItem = inventory[slotId];
            usedItem.Use();
            TryUpdateUI(slotId, usedItem, true);
            inventory[slotId] = null;
        }
    }

    public List<Item> GetItems()
    {
        return inventory.ToList();
    }
}
