using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStation : InteractableBehaviour
{
    public DayManager dm;

    public List<Item> itemsAtStation;

    public bool automagic = true;

    public Box boxedBox;

    public Item boxItem;

    public Inventory holder;

    public List<GameObject> modelsOnStation;

    private GameObject boxModel;

    void Start()
    {
        GameObject go = Instantiate(this.boxItem.model, Vector3.zero,
                Quaternion.identity, this.gameObject.transform);
        go.transform.localPosition = new Vector3(0, 0.77f, 0);
        go.transform.Rotate(new Vector3(0, 90f, 0));
        boxModel = go;
        boxModel.SetActive(false);
    }

    public Box PlaceItemAtStation(Holdable item)
    {
        if (item.IsBox())
        {
            return null;
        }
        
        if (boxedBox != null)
        {
            return boxedBox;
        }

        itemsAtStation.Add(item.ItemReference);
        holder.Push(item.ItemReference);

        if (automagic)
        {
            return BoxOrder();
        }

        return null;
    }

    public Holdable RemoveFirstItemFromStation()
    {
        Item i = itemsAtStation[itemsAtStation.Count - 1];
        itemsAtStation.Remove(i);
        Destroy(holder.Pop());
        return Holdable.FromItem(i);
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
                Destroy(holder.Pop());
            }

            // The items are being boxed, they're gone
            itemsAtStation.RemoveRange(0, itemsAtStation.Count);

            boxModel.SetActive(true);

            boxedBox = box;
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
            if (!o.Completed)
            {
                bool check = o.CheckAgainstOrder(itemsAtStation);

                if (check == true)
                {
                    return o;
                }
            }
        }

        return null;
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        PlaceItemAtStation(player.ReleaseHoldableItem());
    }

    public override void PrimaryAction(PlayerMovement player)
    {
        if (boxedBox != null && player.CanHoldBox())
        {
            player.HoldItem(boxedBox);
            boxModel.SetActive(false);
            boxedBox = null;
        } else if (itemsAtStation.Count > 0 && player.CanHoldItem())
        {
            player.HoldItem(RemoveFirstItemFromStation());
        }
    }

    public override bool SecondaryActionAllowed(PlayerMovement player)
    {
        return (boxedBox == null && player.HasItem());
    }

    public override bool PrimaryActionAllowed(PlayerMovement player)
    {
        return ((boxedBox != null && player.CanHoldBox() 
            || itemsAtStation.Count > 0) && player.CanHoldItem());
    }
}
