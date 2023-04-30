using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : InteractableBehaviour
{
    public override void PrimaryAction(PlayerMovement player)
    {
        if (!player.holdingBox)
        {
            player.ReleaseHoldableItem();
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        PrimaryAction(player);
    }
}
