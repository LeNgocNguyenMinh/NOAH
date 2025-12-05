using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]private Item item; //Contain item information
    [SerializeField]private int itemQuantity;
    [SerializeField]private SpriteRenderer spriteRenderer;
    private bool isCollected = false;
    private Animator animator;
    void Start()
    {
        spriteRenderer.sprite = item.itemSprite;
    }
    public string GetItemID()
    {
        return item.itemID;
    }
    public Vector3 GetItemPos()
    {
        return transform.position;
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
            animator = GetComponent<Animator>();
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
        animator = GetComponent<Animator>();
        animator.SetTrigger("Drop");
    }
}
