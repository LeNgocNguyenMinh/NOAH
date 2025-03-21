using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]private Item item; //Contain item information
    [SerializeField]private int itemQuantity;
    private SpriteRenderer spriteRenderer;
    private UIInventoryPage uiInventoryPage;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;

        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag=="Player")
        {
            if(uiInventoryPage.AddItem(item, itemQuantity))
            {
                uiInventoryPage.AddItemPopUp(item, itemQuantity);
                Destroy(gameObject);
            }
        }
    }
}
