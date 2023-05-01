using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : InteractableBehaviour
{
    public override void PrimaryAction(PlayerMovement player)
    {
        
    }

    public override bool PrimaryActionAllowed(PlayerMovement player)
    {
        return false;
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        if (!player.holdingBox)
        {
            player.ReleaseHoldableItem();
        }
    }

    public override bool SecondaryActionAllowed(PlayerMovement player)
    {
        return player.HasItem();
    }
}
