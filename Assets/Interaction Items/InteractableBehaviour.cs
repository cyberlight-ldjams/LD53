using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public abstract class InteractableBehaviour : MonoBehaviour
{
    UIPrompts prompter;
    [SerializeField]
    private GameObject canvasPrefab;


    public void ShowInteractions(PlayerMovement pm)
    {

        if(prompter == null)
        {
            GameObject canvas = Instantiate(canvasPrefab, transform);
            canvas.transform.Translate(Vector3.up, Space.Self);
            prompter = canvas.GetComponentInChildren<UIPrompts>();
        }

        if(PrimaryActionAllowed(pm))
        {
            prompter.TogglePrompt(UIPrompts.Prompt.PRIMARY, true);
        }

        if (SecondaryActionAllowed(pm))
        {
            prompter.TogglePrompt(UIPrompts.Prompt.SECONDARY, true);
        }
    }

    public void HideInteractions()
    {
        prompter.TogglePrompt(UIPrompts.Prompt.PRIMARY, false);
        prompter.TogglePrompt(UIPrompts.Prompt.SECONDARY, false);
    }

    private void FindPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();

            ShowInteractions(pm);
            pm.SetCurrentInteractable(this);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        FindPlayer(other);
    }

    protected void OnTriggerStay(Collider other)
    {
        FindPlayer(other);
    }

    protected void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HideInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(null);
        }
    }

    public abstract void PrimaryAction(PlayerMovement player);

    public abstract void SecondaryAction(PlayerMovement player); 

    public abstract bool SecondaryActionAllowed(PlayerMovement player);

    public abstract bool PrimaryActionAllowed(PlayerMovement player);

    public enum InteractionState
    {
        AVAILABLE,
        STARTED,
        NONE
    }
    
}
