using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]private Item item; //Contain item information
    [SerializeField]private int itemQuantity;
    private ItemInGroundController itemInGroundController;
    private SpriteRenderer spriteRenderer;
    private UIInventoryPage uiInventoryPage;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
        itemInGroundController = FindObjectOfType<ItemInGroundController>().GetComponent<ItemInGroundController>();
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
    }
    public string GetItemID()
    {
        return item.itemID;
    }
    public Vector3 GetItemPos()
    {
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="Player")
        {
            if(uiInventoryPage.AddItem(item, itemQuantity))
            {
                uiInventoryPage.AddItemPopUp(item, itemQuantity);
                itemInGroundController.SetItemIsCollect(item.itemID, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
