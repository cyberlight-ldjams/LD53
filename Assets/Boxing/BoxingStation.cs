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

    public List<GameObject> modelsOnStation;

    private GameObject boxModel;

    void Start()
    {
        GameObject go = Instantiate(this.boxItem.model, Vector3.zero,
                Quaternion.identity, this.gameObject.transform);
        go.transform.localPosition = new Vector3(0, 0.67f, 0);
        go.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        boxModel = go;
        boxModel.SetActive(false);
    }

    public Box PlaceItemAtStation(Holdable item)
    {
        if (boxedBox != null)
        {
            return boxedBox;
        }

        itemsAtStation.Add(item.ItemReference);

        if (automagic)
        {
            return BoxOrder();
        }

        return null;
    }

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
        } else if (boxedBox != null)
        {
            player.HoldItem(boxedBox);
            boxModel.SetActive(false);
            boxedBox = null;
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        if (itemsAtStation.Count > 0 && player.CanHoldItem())
        {
            player.HoldItem(RemoveFirstItemFromStation());
        }
    }
}
