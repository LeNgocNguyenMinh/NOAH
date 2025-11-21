using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractive : MonoBehaviour 
{
    [SerializeField]private Item item;
    private ObjectInteraction objectInteraction;
    private Animator animator;
    private bool chestOpen = false;
    public void Update()
    {
        objectInteraction = GetComponent<ObjectInteraction>();
        if(objectInteraction.GetCanInteract())
        {
            if(Input.GetKeyDown(KeyCode.F) && !chestOpen)
            {
                animator = GetComponent<Animator>();
                chestOpen = true;
                animator.SetTrigger("chestIsOpen");
                if (!UIInventoryPage.Instance.AddItem(item, 1))
                {
                    return;
                }
                UIInventoryPage.Instance.AddItemPopUp(item, 1);
                ChestIsEmpty();
            }
        }
    }
    public void ChestIsEmpty()
    {
        Destroy(gameObject, 2f);
    }
}
