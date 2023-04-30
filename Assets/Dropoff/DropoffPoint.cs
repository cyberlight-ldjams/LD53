using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DropoffPoint : InteractableBehaviour
{
    public DayManager dm;

    public PlayerMovement pm;

    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public Order CompleteOrder(Box box)
    {
        foreach (Order o in dm.orderList)
        {
            if (o.CheckAgainstOrder(box.Items))
            {
                return dm.CompleteOrder(o);
            }
        }

        // We should never reach here
        // Unless we decide to have the player
        // be able to box inaccurate orders
        return null;
    }

    public override void PrimaryAction()
    {
        if (pm.holdingBox)
        {
            Item held = pm.ReleaseItem();
            // Make sure this is a box
            if (held.isBox)
            {
                CompleteOrder((Box) held);
            }
            // If not, give it back to the player
            else
            {
                pm.HoldItem(held);
            }
        }
    }

    public override void SecondaryAction()
    {
        // Do nothing
    }
}
