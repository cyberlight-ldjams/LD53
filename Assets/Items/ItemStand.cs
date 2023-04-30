using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStand : InteractableBehaviour
{

    public GameObject itemOnStand;

    public Item item;

    public ParticleSystem poofer;


    public bool itemTaken { get; private set; }

    // Use this if the item's position on the stand needs to be modified - added
    public Vector3 positionCorrection = new Vector3(0f, 0f, 0f);

    // Time to wait before particle effect
    private readonly float timeToWait1 = 1f;

    // Time to wait before item reappears
    private readonly float timeToWait2 = 2f;

    private float timeSinceTaken = 0f;

    void Awake()
    {
        poofer.Pause();
    }

    void Start()
    {
       itemOnStand = item.Rescale(transform, positionCorrection);

    }

    void Update()
    {
        if (itemTaken)
        {
            timeSinceTaken += Time.deltaTime;

            if (timeSinceTaken > timeToWait1)
            {
                Poof();
            }

            if (timeSinceTaken > timeToWait2)
            {
                itemOnStand.GetComponentInChildren<MeshRenderer>().enabled = true;
                itemTaken = false;
            }
        }

    }

    public void Poof()
    {
        poofer.Play();
    }

    public Holdable TakeItem()
    {
        itemOnStand.GetComponentInChildren<MeshRenderer>().enabled = false;
        itemTaken = true;
        timeSinceTaken = 0f;

        return Holdable.FromItem(item); 
    }

    public override void PrimaryAction(PlayerMovement player)
    {
        if (!itemTaken && player.CanHoldItem())
        {
            player.HoldItem(TakeItem());
            PlayerAnimation pa = player.gameObject.GetComponent<PlayerAnimation>();
            pa.hold = true;
            pa.pickUp = true;
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        // Do nothing
    }
}
