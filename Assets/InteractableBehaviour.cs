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
        Debug.Log("Show a popup?");
    }

    public void HideInteractions()
    {
        //
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("Player"))
        {
            ShowInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(this);
        } else
        {
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HideInteractions();
            other.gameObject.GetComponent<PlayerMovement>().SetCurrentInteractable(null);
        }
    }

    public abstract void PrimaryAction();

    public abstract void SecondaryAction(); 

    
}
