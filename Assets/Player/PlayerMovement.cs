using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{

    [Range(0f, 0.99f)]
    [SerializeField]
    private float smoothing = 0.25f;

    [SerializeField]
    private float targetLerpSpeed = 1f;

    private NavMeshAgent agent;


    private Vector3 _movement, _lastDirection, _targetDirection;
    private float lerpTime = 0f;

    private PlayerInput controls;
    private InputAction primary, secondary;

    private InteractableBehaviour currentInteractable = null;


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
        currentInteractable.PrimaryAction();
    }

    private void OnSecondaryAction(InputValue input)
    {
        currentInteractable.SecondaryAction();
    }
}
