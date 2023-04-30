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

    // Use this if the scale needs to be modified - multiplied
    public Vector3 scaleCorrection = new Vector3(1f, 1f, 1f);

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
        if (item.model != null)
        {
            Transform t = this.gameObject.transform;
            itemOnStand = Instantiate(item.model Vector3.zero,
                Quaternion.identity, t);
            Vector3 localPosition = new Vector3(0f + positionCorrection.x,
                1.5f + positionCorrection.y, 0f + positionCorrection.z);
            Debug.Log("LocPos: " + localPosition);
            itemOnStand.transform.localPosition = localPosition;
            itemOnStand.transform.localScale = new Vector3(
                (1f / t.localScale.x) * item.model.transform.localScale.x * scaleCorrection.x,
                (1f / t.localScale.y) * item.model.transform.localScale.y * scaleCorrection.y,
                (1f / t.localScale.z) * item.model.transform.localScale.z * scaleCorrection.z);
        }
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
        /**
        Item i = new Item();
        i.itemName = item.itemName;
        i.description = item.description;
        i.value = item.value;
        i.model = item.model;
        i.isBox = item.isBox;
        */
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
        }
    }

    public override void SecondaryAction(PlayerMovement player)
    {
        // Do nothing
    }
}
