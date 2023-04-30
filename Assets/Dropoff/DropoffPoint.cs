using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DropoffPoint : InteractableBehaviour
{
    public DayManager dm;

    public Order CompleteOrder(Box box)
    {
        foreach (Order o in dm.orderList)
        {
            if (!o.Completed && o.CheckAgainstOrder(box.Items))
            {
                return dm.CompleteOrder(o);
            }
        }

        // We should never reach here
        // Unless we decide to have the player
        // be able to box inaccurate orders
        return null;
    }

    public override void PrimaryAction(PlayerMovement player)
    {
        if (player.holdingBox)
        {
            Holdable held = player.ReleaseHoldableItem();
            // Make sure this is a box
            if (held.IsBox())
            {
                CompleteOrder((Box) held);
                player.ReleaseHoldableItem();
            }
            // If not, give it back to the player
            else
            {
                player.HoldItem(held);
            }
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        // Do nothing
    }
}
