using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private bool canInteract = false;
    public bool GetCanInteract()
    {
        return canInteract;
    }
    public void SetCanInteract(bool newValue)
    {
        canInteract = newValue;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
