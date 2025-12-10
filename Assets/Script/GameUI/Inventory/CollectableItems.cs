using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]private Item item; //Contain item information
    private int itemQuantity;
    [SerializeField]private Animator animator;
    [SerializeField]private TextMeshPro itemQuantityText;
    private bool isCollected = false;
    private void Start()
    {
        if(itemQuantityText != null)
        {
            itemQuantityText.text = GetItemQuantity().ToString() + "";
        }
    }
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
        if(itemQuantity == 0)
        {
            return 1;
        }
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
                Destroy(transform.root.gameObject, .5f);
            }
        }
    }
    
    public void DropItemAnim()
    {
        animator.SetTrigger("Drop");
    }
}
