using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Order
{
    private List<Item> possibleItems;

    private List<Item> orderItems;

    public Order(List<Item> possibleItems, float orderSize)
    {
        this.possibleItems = possibleItems;

        orderItems = new List<Item>(orderItems);

        for (int i = 0; i < orderSize; i++)
        {
            int nextItem = Random.Range(0, possibleItems.Count);
            orderItems.Add(possibleItems[nextItem]);
        }
    }

    public void AddOrderItem(Item item)
    {
        orderItems.Add(item);
    }

    public bool CheckAgainstOrder(List<Item> items)
    {
        // If the counts aren't the same, obviously the lists aren't the same
        if (items.Count != orderItems.Count)
        {
            return false;
        }

        int count = items.Count;

        List<string> itemNames = new List<string>(count);
        List<string> orderItemNames = new List<string>(count);

        // Get all the item names
        for (int i = 0; i < count; i++)
        {
            itemNames.Add(items[i].itemName);
            orderItemNames.Add(orderItems[i].itemName);
        }

        // Sort the lists
        itemNames.Sort();
        orderItemNames.Sort();

        for (int i = 0; i < count; i++)
        {
            if (!itemNames[i].Equals(orderItemNames[i])) {
                return false;
            }
        }

        return true;
    }
}
