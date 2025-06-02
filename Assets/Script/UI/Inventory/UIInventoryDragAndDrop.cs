using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIInventoryDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIInventoryItem uiInventoryItem;
    private UIInventoryPage uiInventoryPage;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Canvas canvasParent;
    private Vector3 originalLocalPosition;
    private void Start()
    {
        canvasParent = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        uiInventoryItem = GetComponentInParent<UIInventoryItem>();
        originalParent = transform.parent;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        uiInventoryPage = FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        uiInventoryPage.OnlyClickOneSlot();
        uiInventoryPage.OnlySellectOneSlot();
        if(uiInventoryItem == null || uiInventoryItem.isEmpty) return;
        originalLocalPosition = transform.localPosition;
        transform.SetParent(canvasParent.transform, true);
        //Make item blur a bit
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Make item visible
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        UIInventoryItem newSlot = eventData.pointerEnter?.GetComponentInParent<UIInventoryItem>();
        UIInventoryItem previousSlot = originalParent.GetComponent<UIInventoryItem>();

        if(newSlot!=null && newSlot != previousSlot)//If there is a slot under drop point
        {
            if(newSlot.hotBarSlot && previousSlot.GetItemID().Contains("Cloth"))
            {
                PopUp.Instance.ShowNotification("Can't add cloth to hot bar");
                transform.SetParent(originalParent, true);
                transform.localPosition = originalLocalPosition;
                return;
            }
            if(newSlot.hotBarSlot && previousSlot.GetItemID().Contains("Note"))
            {
                PopUp.Instance.ShowNotification("Can't add paper to hot bar");
                transform.SetParent(originalParent, true);
                transform.localPosition = originalLocalPosition;
                return;
            }
            if(newSlot.isEmpty) //If no item in new slot
            {
                transform.SetParent(previousSlot.transform, true);
                transform.localPosition = Vector3.zero;
                newSlot.AddItem(previousSlot.GetItem(), previousSlot.GetItemQuantity());
                previousSlot.DeleteItem();   // Delete the item info in old slot
            }
            else{//Mean we will swap items
                //Save item info
                if(newSlot.GetItemID() == previousSlot.GetItemID())
                {
                    newSlot.AddQuantity(previousSlot.GetItemQuantity());
                    previousSlot.DeleteItem();
                }
                else{
                    Item item = newSlot.GetItem();
                    int itemQuantity = newSlot.GetItemQuantity();
                
                    transform.SetParent(previousSlot.transform, true);
                    transform.localPosition = Vector3.zero;
                    
                    newSlot.DeleteItem();//Delete item info in second slot  

                    newSlot.AddItem(previousSlot.GetItem(), previousSlot.GetItemQuantity());
                    previousSlot.DeleteItem(); //Delete item info in first slot

                    previousSlot.AddItem(item, itemQuantity);
                }
            }
        }
        else//Deliver the item back to its slot
        {
            if(eventData.pointerEnter == null)//mean you drop it outside inv
            {
                Transform playerTrans = FindObjectOfType<PlayerControl>().transform;
                Vector2 dropOffset = Random.insideUnitCircle.normalized * 2f; // Random offset to avoid items stacking on each other
                Vector2 dropPosition = (Vector2)playerTrans.position + dropOffset;
                CollectableItems dropItem = Instantiate(previousSlot.GetItem().itemPrefab, dropPosition, Quaternion.identity).GetComponentInChildren<CollectableItems>();
                dropItem.SetItemQuantity(previousSlot.GetItemQuantity());
                dropItem.DropItemAnim();
                transform.SetParent(previousSlot.transform, true);
                transform.localPosition = Vector3.zero;
                previousSlot.DeleteItem();
                return;
            }
            transform.SetParent(originalParent, true);
            transform.localPosition = originalLocalPosition;
        } 
    }
}
