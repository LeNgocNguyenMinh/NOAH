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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canInteract = true;
            if(!Player.Instance.InteractRemind.activeSelf)
            {
                Player.Instance.InteractRemind.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            canInteract = false;
            if(Player.Instance.InteractRemind.activeSelf)
            {
                Player.Instance.InteractRemind.SetActive(false);
            }
        }
    }
}
