using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStation : InteractableBehaviour
{
    public DayManager dm;

    public List<Item> itemsAtStation;

    public bool automagic = true;

    public Box PlaceItemAtStation(Holdable item)
    {
        itemsAtStation.Add(item.ItemReference);

        if (automagic)
        {
            return BoxOrder();
        }

        return null;
    }

    //TODO:  This has not been converted to Holdables
    //QUESTION: When we hold the items from this station, does it respect capacity??
    public Holdable RemoveFirstItemFromStation()
    {
        foreach (Item i in itemsAtStation)
        {
            itemsAtStation.Remove(i);
            return Holdable.FromItem(i);
        }

        return null;
    }

    public Holdable RemoveItemFromStation(Item item)
    {
        foreach (Item i in itemsAtStation)
        {
            if (i.itemName.Equals(item.itemName))
            {
                itemsAtStation.Remove(i);

                return Holdable.FromItem(i);
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

    public override void PrimaryAction(PlayerMovement player)
    {
        if(player.HasItem())
        {
            PlaceItemAtStation(player.ReleaseHoldableItem());
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        if (itemsAtStation.Count > 0)
        {
            player.HoldItem(RemoveFirstItemFromStation());
        }
    }
}
