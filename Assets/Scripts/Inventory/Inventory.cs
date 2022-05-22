using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite commonInventorySlotIcon;

    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        if(items.Count < 3)
        {
            HideObject(item);
            items.Add(item);
        }
    }

    private void HideObject(Item item)
    {
        item.gameObject.SetActive(false);  
    }

    public void UseItem(int slotId)
    {
        if (items[slotId] != null)
        {
            Item usedItem = items[slotId];
            usedItem.Use();
            items.Remove(items[slotId]);
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
