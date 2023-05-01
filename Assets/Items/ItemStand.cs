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
        ReplaceItem(item);
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
                MeshRenderer[] renderers = itemOnStand.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in renderers)
                {
                    mr.enabled = true;
                }
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
        MeshRenderer[] renderers = itemOnStand.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = false;
        }

        itemTaken = true;
        timeSinceTaken = 0f;

        return Holdable.FromItem(item); 
    }

    public void ReplaceItem(Item item)
    {
        itemOnStand = item.Rescale(transform, positionCorrection);
        MeshRenderer[] renderers = itemOnStand.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in renderers)
        {
            mr.enabled = true;
        }

        itemTaken = false;
        timeSinceTaken = 0f;
    }

    public override bool PrimaryActionAllowed(PlayerMovement player)
    {
        return !itemTaken && player.CanHoldItem();
    }

    public override bool SecondaryActionAllowed(PlayerMovement player)
    {
        return (itemTaken && player.HasItem());
    }

    public override void PrimaryAction(PlayerMovement player)
    {
        //validation logic is now handled via the  *ActionAllowed() as it is needed elsewhere.
        player.HoldItem(TakeItem());
        PlayerAnimation pa = player.gameObject.GetComponent<PlayerAnimation>();
        pa.hold = true;
        pa.pickUp = true;

    }

    public override void SecondaryAction(PlayerMovement player)
    {
        //validation logic is now handled via the  *ActionAllowed() as it is needed elsewhere.
        ReplaceItem(player.ReleaseHoldableItem().ItemReference);
    }

    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        PopupManager.Instance.ShowPopup(item);
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        PopupManager.Instance.HidePopup();
    }

    

    

}
