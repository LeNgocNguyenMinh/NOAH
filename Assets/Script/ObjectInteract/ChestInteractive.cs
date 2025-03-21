using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractive : MonoBehaviour 
{
    [SerializeField]private Item item;
    private Animator animator;
    private UIInventoryPage uiInventoryPage;
    private bool chestOpen = false;
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.F) && !chestOpen)
            {
                animator = GetComponent<Animator>();
                uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
                if (!uiInventoryPage.AddItem(item, 1))
                {
                    return;
                }
                uiInventoryPage.AddItemPopUp(item, 1);
                chestOpen = true;
                animator.SetTrigger("chestIsOpen");
                ChestIsEmpty();
            }
        }
    }
    public void ChestIsEmpty()
    {
        Destroy(gameObject, 1f);
    }
}
