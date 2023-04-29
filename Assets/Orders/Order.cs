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


        return false;
    }
}
