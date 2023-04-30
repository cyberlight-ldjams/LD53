using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{

    [Range(0f, 0.99f)]
    [SerializeField]
    private float smoothing = 0.25f;

    [SerializeField]
    private float targetLerpSpeed = 1f;

    [SerializeField]
    private List<Holdable> heldItems;

    [SerializeField]
    [Min(1)]
    private int holdCapacity = 1;

    [SerializeField]
    public bool holdingBox { get; private set; } = false;

    [SerializeField]
    private GameObject orderList;

    private NavMeshAgent agent;

    private Vector3 _movement, _lastDirection, _targetDirection;
    private float lerpTime = 0f;

    private PlayerInput controls;
    private InputAction primary, secondary;

    private InteractableBehaviour currentInteractable = null;


    /**
     * @
     */
    public bool HasItem(Item item = null)
    {
        if (item == null)
        {
            return heldItems.Count > 0;
        } else
        {
            Holdable myItem = heldItems.Find((Holdable h) =>
            {
                return (h.ItemReference == item);
            });

            return myItem != null;
        }
    }

    //This is triggered automatically by the PlayerInput component when the Move action occurs.
    void OnMove(InputValue inputValue) { 
        Vector2 input = inputValue.Get<Vector2>();
        _movement = new Vector3(input.x, 0, input.y);
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controls = GetComponent<PlayerInput>();
        primary = controls.actions.FindAction("PrimaryAction");
        secondary = controls.actions.FindAction("SecondaryAction");
        SetCurrentInteractable(null);
        
        //Shouldn't ever actually have to do this, but in case it disappears again...
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void Start()
    {
        heldItems = new List<Holdable>();
    }

    void Update()
    {
        _movement.Normalize();
        if(_movement != _lastDirection)
        {
            lerpTime = 0f;
        }
        float smoothedLerpTime = Mathf.Clamp01(lerpTime * targetLerpSpeed * (1 - smoothing));
        _lastDirection = _movement;
        _targetDirection = Vector3.Lerp(_targetDirection, _movement, smoothedLerpTime);

        agent.Move(_targetDirection * agent.speed * Time.deltaTime);
        Vector3 lookDir = _movement;
        if(lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.LookRotation(lookDir), smoothedLerpTime);
        }

        lerpTime += Time.deltaTime;
    }

    public void HoldItem(Holdable item)
    {
        if (item.IsBox())
        {
            if (heldItems.Count == 0)
            {
                holdingBox = true;
                heldItems.Add(item);
            }
        } else if (CanHoldItem())
        {
            item.heldModel = Instantiate(item.ItemReference.model, transform);
            item.heldModel.transform.localPosition = new Vector3(0, 0, 0.5f);
            heldItems.Add(item);


        } // else, do nothing
    }

    public bool CanHoldItem()
    {
        return (!holdingBox && heldItems.Count < holdCapacity);
    }

    public Holdable ReleaseHoldableItem()
    {
        if(heldItems.Count == 0)
        {
            return null;
        }

        Holdable held = heldItems[heldItems.Count - 1];

        // Return the first item in hand
        
        if (held.IsBox())
        {
            holdingBox = false;
        }
        heldItems.Remove(heldItems[heldItems.Count - 1]);

        //TODO: Object pooling?
        Destroy(held.heldModel);

        return held;
        

        // Else, we have no items
        return null;
    }

    public Item ReleaseItem(Item item)
    {
        // Return the first item in hand that matches
        for (int i = 0; i < heldItems.Count; i++)
        {
            if (heldItems[i].ItemReference.itemName.Equals(item.itemName))
            {
                if (heldItems[i].IsBox())
                {
                    holdingBox = false;
                }
                Item held = heldItems[i].ItemReference;
                heldItems.Remove(heldItems[i]);
                return held;
            }
        }

        // Else, we have no items
        return null;
    }

    public void SetCurrentInteractable(InteractableBehaviour current)
    {
        currentInteractable = current;
        if(currentInteractable != null)
        {
            primary.Enable();
            secondary.Enable();
        } else
        {
            primary.Disable();
            secondary.Disable();
        }
    }

    private void OnPrimaryAction(InputValue input)
    {
        currentInteractable.PrimaryAction(this);
    }

    private void OnSecondaryAction(InputValue input)
    {
        currentInteractable.SecondaryAction(this);
    }

    private void OnToggleOrders(InputValue input)
    {
        if (orderList != null)
        {
            orderList.SetActive(!orderList.activeSelf);
        }
    }
}
