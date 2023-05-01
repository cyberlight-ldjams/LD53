using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider), typeof(Canvas))]
public abstract class InteractableBehaviour : MonoBehaviour
{
    private Canvas canvas;


    [Header("Interaction Canvas Elements")]
    [SerializeField]
    TextMeshProUGUI primaryPrompt, primaryText, secondaryPrompt, secondaryText;


    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    public void ShowInteractions()
    {
        //
    }

    public void HideInteractions()
    {
        //
    }

    private void FindPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(this);
        }
        else
        {
            Debug.Log(other.name);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        FindPlayer(other);
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(this);
        }
        else
        {
            Debug.Log(other.name);
        }
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
