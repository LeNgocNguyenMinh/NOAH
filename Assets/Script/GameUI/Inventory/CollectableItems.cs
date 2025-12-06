using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]private Item item; //Contain item information
    [SerializeField]private int itemQuantity;
    [SerializeField]private Animator animator;
    private bool isCollected = false;
    public string GetItemID()
    {
        return item.itemID;
    }
    public Vector3 GetItemPos()
    {
        return transform.position;
    }
    public int GetItemQuantity()
    {
        return itemQuantity;
    }
    public void SetItemQuantity(int quantity)
    {
        itemQuantity = quantity;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(isCollected == true) return;
        if(collider.CompareTag("Player"))
        {
            animator.SetTrigger("Collect");
            if(UIInventoryPage.Instance.AddItem(item, itemQuantity))
            {
                isCollected = true;
                UIInventoryPage.Instance.AddItemPopUp(item, itemQuantity);
                ItemInGroundController.Instance.SetItemIsCollect(item.itemID, transform.position);
                Destroy(gameObject, .5f);
            }
        }
    }
    
    public void DropItemAnim()
    {
        animator.SetTrigger("Drop");
    }
}
