using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Order
{
    private List<Item> possibleItems;

    public List<Item> orderItems { get; private set; }

    public bool Completed { get; private set; }

    public Order(List<Item> possibleItems, int orderSize)
    {
        this.possibleItems = possibleItems;

        orderItems = new List<Item>(orderSize);

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

    public int GetOrderItemCount()
    {
        return orderItems.Count;
    }

    public Item GetOrderItem(int item)
    {
        if (item < orderItems.Count)
        {
            return orderItems[item];
        } else
        {
            return null;
        }
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

    public Order MarkCompleted()
    {
        Completed = true;
        return this;
    }

    public float GetOrderValue()
    {
        float value = 0;
        foreach (Item i in orderItems)
        {
            value += i.value;
        }
        return value;
    }
}
