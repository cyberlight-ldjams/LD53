using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStation : MonoBehaviour
{
    public DayManager dm;

    public List<Item> itemsAtStation;

    public bool automagic = true;

    public Box PlaceItemAtStation(Item item)
    {
        itemsAtStation.Add(item);

        if (automagic)
        {
            return BoxOrder();
        }

        return null;
    }

    public Item RemoveItemFromStation(Item item)
    {
        foreach (Item i in itemsAtStation)
        {
            if (i.itemName.Equals(item.itemName))
            {
                itemsAtStation.Remove(i);

                return i;
            }
        }

        return null;
    }

    public Box BoxOrder()
    {
        Order completed = CheckAgainstAllOrders();

        if (completed != null)
        {
            Box box = new Box(completed);

            foreach (Item i in itemsAtStation)
            {
                box.AddItem(i);
            }

            return box;
        } // else

        return null;
    }

    // Checks to see if the items at the station
    // Correctly correspond to any order
    public Order CheckAgainstAllOrders()
    {
        foreach (Order o in dm.orderList)
        {
            bool check = o.CheckAgainstOrder(itemsAtStation);

            if (check == true)
            {
                return o;
            }
        }

        return null;
    }
}
