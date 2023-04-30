using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public abstract class InteractableBehaviour : MonoBehaviour
{

    public void ShowInteractions()
    {
        //
    }

    public void HideInteractions()
    {
        //
    }

    protected void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ShowInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(this);
        } else
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

    
}
