using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DropoffPoint : MonoBehaviour
{
    public DayManager dm;

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
}
