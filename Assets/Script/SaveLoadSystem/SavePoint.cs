using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Animator animator;
    private PlayerControl playerControl;
    private ObjectInteraction objectInteraction;
    private bool triggerNearAnimation = false;
    private bool triggerFarAnimation = false;
    private void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SaveController.Instance.SaveGame();
            }
            if(!triggerNearAnimation)
            {
                triggerNearAnimation = true;
                triggerFarAnimation = false;
                animator = GetComponent<Animator>();
                animator.SetTrigger("isNear");
            }
        }
        else
        {
            if(!triggerFarAnimation)
            {
                triggerNearAnimation = false;
                triggerFarAnimation = true;
                animator = GetComponent<Animator>();
                animator.SetTrigger("isFar");
            }
        }
    }
}
